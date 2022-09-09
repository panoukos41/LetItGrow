using DynamicData;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Notifications;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.UI.Common.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Group.Services
{
    public class GroupService : IGroupService
    {
        private bool tryLoad = true;

        private readonly SourceCache<GroupModel, string> _groups = new(group => group.Id);
        private readonly HubService hub;

        public GroupService(HubService hub)
        {
            this.hub = hub;
            this.hub.On<GroupCreated>("group:added", GroupAdded);
            this.hub.On<GroupUpdated>("group:updated", GroupUpdated);
            this.hub.On<GroupDeleted>("group:removed", GroupRemoved);
        }

        /// <inheritdoc/>
        public IEnumerable<GroupModel> Cache
        {
            get
            {
                if (tryLoad) Connect();
                return _groups.Items;
            }
        }

        private void GroupAdded(GroupCreated notification)
        {
            Console.WriteLine("Group Added");
            _groups.AddOrUpdate(notification.Group);
        }

        private void GroupUpdated(GroupUpdated notification) =>
            _groups.AddOrUpdate(notification.Group);

        private void GroupRemoved(GroupDeleted notification) =>
            _groups.RemoveKey(notification.GroupId);

        /// <inheritdoc/>
        public IObservable<IChangeSet<GroupModel, string>> Connect(Func<GroupModel, bool>? predicate = null)
        {
            if (tryLoad)
            {
                tryLoad = false;
                _ = Task.Run(async () =>
                {
                    var result = await Search(new());
                    result.Switch(
                        s => _groups.EditDiff(s, static (l, r) => l.Id == r.Id),
                        e =>
                        {
                            Console.WriteLine(e);
                            tryLoad = true;
                        });
                });
            }

            return _groups.Connect(predicate);
        }

        /// <inheritdoc/>
        public IObservable<GroupModel> Watch(string id) =>
            _groups.WatchValue(id);

        /// <inheritdoc/>
        public async ValueTask<Result<GroupModel>> Get(string id)
        {
            var lookup = _groups.Lookup(id);

            if (lookup.HasValue) return lookup.Value;

            var result = await hub.SendAsync("group:get", new FindGroup(id));
            result.Switch(
                s => _groups.AddOrUpdate(s),
                e => Console.WriteLine(e));

            return result;
        }

        /// <inheritdoc/>
        public Task<Result<GroupModel[]>> Search(SearchGroups request) =>
            hub.SendAsync("group:search", request);

        /// <inheritdoc/>
        public Task<Result<GroupModel>> Create(CreateGroup request) =>
            hub.SendAsync("group:create", request);

        /// <inheritdoc/>
        public Task<Result<ModelUpdate>> Update(UpdateGroup request) =>
            hub.SendAsync("group:update", request);

        /// <inheritdoc/>
        public Task<Result<Unit>> Delete(DeleteGroup request) =>
            hub.SendAsync("group:delete", request);
    }
}