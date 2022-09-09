using DynamicData;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.UI.Common.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Unit = MediatR.Unit;

namespace LetItGrow.UI.Node.Services
{
    public class NodeService : INodeService
    {
        private bool tryLoad = true;
        private ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> LoadNodes { get; }

        private readonly SourceCache<NodeModel, string> _nodes = new(node => node.Id);
        private readonly HubService hub;

        public NodeService(HubService hub)
        {
            this.hub = hub;
            hub.On<NodeCreated>("node:added", NodeAdded);
            hub.On<NodeUpdated>("node:updated", NodeUpdated);
            hub.On<NodeDeleted>("node:removed", NodeRemoved);

            LoadNodes = ReactiveCommand.CreateFromTask(() =>
                Search(new()).SwitchAsync(
                    s =>
                    {
                        _nodes.AddOrUpdate(s);
                        tryLoad = false;
                    },
                    e => Console.WriteLine(e)));
        }

        /// <inheritdoc/>
        public IEnumerable<NodeModel> Cache
        {
            get
            {
                if (tryLoad) LoadNodes.Execute().Subscribe();
                return _nodes.Items;
            }
        }

        /// <inheritdoc/>

        private void NodeAdded(NodeCreated notification)
        {
            Console.WriteLine("Node Added");
            _nodes.AddOrUpdate(notification.Node);
        }

        private void NodeUpdated(NodeUpdated notification) =>
            _nodes.AddOrUpdate(notification.Node);

        private void NodeRemoved(NodeDeleted notification) =>
            _nodes.RemoveKey(notification.NodeId);

        /// <inheritdoc/>
        public IObservable<IChangeSet<NodeModel, string>> Connect(Func<NodeModel, bool>? predicate = null)
        {
            if (tryLoad) LoadNodes.Execute().Subscribe();

            return _nodes.Connect(predicate);
        }

        /// <inheritdoc/>
        public IObservable<NodeModel> Watch(string id) =>
            _nodes.WatchValue(id);

        /// <inheritdoc/>
        public async ValueTask<Result<NodeModel>> Get(string id)
        {
            var lookup = _nodes.Lookup(id);
            if (lookup.HasValue)
            {
                return lookup.Value;
            }
            var result = await hub.SendAsync("node:get", new FindNode(id));

            result.Switch(
                s => _nodes.AddOrUpdate(s),
                e => Console.WriteLine(e));

            return result;
        }

        /// <inheritdoc/>
        public Task<Result<NodeModel[]>> Search(SearchNodes request)
        {
            if (!tryLoad)
            {
                var search = request.GroupId is null
                    ? Cache
                    : Cache.Where(x => x.GroupId == request.GroupId);

                return Result<NodeModel[]>.Task(search.ToArray());
            }

            return hub.SendAsync("node:search", request);
        }

        /// <inheritdoc/>
        public Task<Result<NodeModel>> Create(CreateNode request) =>
            hub.SendAsync("node:create", request);

        /// <inheritdoc/>
        public Task<Result<ModelUpdate>> Update(UpdateNode request) =>
            hub.SendAsync("node:update", request);

        /// <inheritdoc/>
        public Task<Result<Unit>> Delete(DeleteNode request) =>
            hub.SendAsync("node:delete", request);

        /// <inheritdoc/>
        public Task<Result<ModelUpdate>> GroupAdd(GroupAdd request) =>
            hub.SendAsync("node:group-add", request);

        /// <inheritdoc/>
        public Task<Result<ModelUpdate>> GroupRemove(GroupRemove request) =>
            hub.SendAsync("node:group-remove", request);

        /// <inheritdoc/>
        public Task<Result<RefreshModel>> Refresh(RefreshToken request) =>
            hub.SendAsync("node:refresh", request);

        /// <inheritdoc/>
        public Task<Result<IrrigationModel[]>> GetIrrigations(SearchIrrigations request) =>
            hub.SendAsync("irrigation:search", request);

        /// <inheritdoc/>
        public Task<Result<IrrigationModel[]>> GetIrrigations(SearchManyIrrigations request) =>
            hub.SendAsync("irrigation:search-many", request);

        /// <inheritdoc/>
        public Task<Result<MeasurementModel[]>> GetMeasurements(SearchMeasurements request) =>
            hub.SendAsync("measurement:search", request);

        /// <inheritdoc/>
        public Task<Result<MeasurementModel[]>> GetMeasurements(SearchManyMeasurements request) =>
            hub.SendAsync("measurement:search-many", request);
    }
}