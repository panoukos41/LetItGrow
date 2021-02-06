using DynamicData;
using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.NodeGroups.Requests;
using LetItGrow.UI.Services.Internal;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public class NodeGroupService : HubServiceBase, INodeGroupService
    {
        private bool tryLoad = true;

        private readonly SourceCache<NodeGroupModel, string> _groups = new(group => group.Id);

        public NodeGroupService(HubConnection hub) : base(hub)
        {
            hub.On<NodeGroupModel>("nodegroup:added", GroupAddedOrUpdated);
            hub.On<NodeGroupModel>("nodegroup:updated", GroupAddedOrUpdated);
            hub.On<string>("nodegroup:removed", GroupRemoved);
        }

        /// <inheritdoc/>
        public IObservable<IChangeSet<NodeGroupModel, string>> Connect(Func<NodeGroupModel, bool>? predicate = null)
        {
            if (tryLoad)
            {
                tryLoad = false;
                _ = Task.Run(async () =>
                {
                    await GetAll(new()).MatchAsync(
                        result => _groups.EditDiff(result, static (l, r) => l.Id == r.Id),
                        error =>
                        {
                            Console.WriteLine(error);
                            tryLoad = true;
                        });
                });
            }

            return _groups.Connect(predicate);
        }

        /// <inheritdoc/>
        public IObservable<NodeGroupModel> Watch(string id) =>
            _groups.WatchValue(id);

        private void GroupAddedOrUpdated(NodeGroupModel group)
        {
            _groups.AddOrUpdate(group);
        }

        private void GroupRemoved(string id)
        {
            _groups.RemoveKey(id);
        }

        /// <inheritdoc/>
        public async ValueTask<Result<NodeGroupModel>> Get(string id)
        {
            var lookup = _groups.Lookup(id);
            if (lookup.HasValue)
            {
                return lookup.Value;
            }
            var result = await Handle("nodegroup:get", new GetNodeGroup(id));
            result.OnSuccess(result => _groups.AddOrUpdate(result));
            return result;
        }

        /// <inheritdoc/>
        public Task<Result<List<NodeGroupModel>>> GetAll(GetNodeGroups request) =>
            Handle("nodegroup:get-all", request);

        /// <inheritdoc/>
        public Task<Result<NodeGroupModel>> Create(CreateNodeGroup request) =>
            Handle("nodegroup:create", request);

        /// <inheritdoc/>
        public Task<Result<NodeGroupModelUpdate>> Update(UpdateNodeGroup request) =>
            Handle("nodegroup:update", request);

        /// <inheritdoc/>
        public Task<Result<Unit>> Delete(DeleteNodeGroup request) =>
            Handle("nodegroup:delete", request);
    }
}