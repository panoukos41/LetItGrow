using DynamicData;
using DynamicData.Kernel;
using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.UI.Services.Internal;
using MediatR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public class NodeService : HubServiceBase, INodeService
    {
        private bool tryLoad = true;

        private readonly SourceCache<NodeModel, string> _nodes = new(node => node.Id);

        public NodeService(HubConnection hub) : base(hub)
        {
            hub.On<NodeModel>("node:added", NodeAddedOrUpdated);
            hub.On<NodeModel>("node:updated", NodeAddedOrUpdated);
            hub.On<string>("node:removed", NodeRemoved);
            hub.On<string, bool>("node:connected", NodeConnected);
        }

        /// <inheritdoc/>
        public IObservable<IChangeSet<NodeModel, string>> Connect(Func<NodeModel, bool>? predicate = null)
        {
            if (tryLoad)
            {
                tryLoad = false;
                _ = Task.Run(async () =>
                {
                    await GetAll(new()).MatchAsync(
                        result => _nodes.EditDiff(result, static (l, r) => l.Id == r.Id),
                        error =>
                        {
                            Console.WriteLine(error);
                            tryLoad = true;
                        });
                });
            }

            return _nodes.Connect(predicate);
        }

        /// <inheritdoc/>
        public IObservable<NodeModel> Watch(string id) =>
            _nodes.WatchValue(id);

        private void NodeAddedOrUpdated(NodeModel node)
        {
            Console.WriteLine($"Added {node}");
            _nodes.AddOrUpdate(node);
        }

        private void NodeRemoved(string id)
        {
            _nodes.RemoveKey(id);
        }

        private void NodeConnected(string id, bool connected)
        {
            _nodes.Lookup(id)
                .IfHasValue(node => _nodes.AddOrUpdate(node with { Connected = connected }));
        }

        /// <inheritdoc/>
        public async ValueTask<Result<NodeModel>> Get(string id)
        {
            var lookup = _nodes.Lookup(id);
            if (lookup.HasValue)
            {
                return lookup.Value;
            }
            var result = await Handle("node:get", new GetNode(id));
            result.OnSuccess(result => _nodes.AddOrUpdate(result));
            return result;
        }

        /// <inheritdoc/>
        public Task<Result<List<NodeModel>>> GetAll(GetNodes request) =>
            Handle("node:get-all", request);

        /// <inheritdoc/>
        public Task<Result<IrrigationNodeModel>> Create(CreateIrrigationNode request) =>
            Handle("node:create-irrigation", request);

        /// <inheritdoc/>
        public Task<Result<MeasurementNodeModel>> Create(CreateMeasurementNode request) =>
            Handle("node:create-measurement", request);

        /// <inheritdoc/>
        public Task<Result<NodeModelUpdate>> Update(UpdateIrrigationNode request) =>
            Handle("node:update-irrigation", request);

        /// <inheritdoc/>
        public Task<Result<NodeModelUpdate>> Update(UpdateMeasurementNode request) =>
            Handle("node:update-measurement", request);

        /// <inheritdoc/>
        public Task<Result<Unit>> Delete(DeleteNode request) =>
            Handle("node:delete", request);

        /// <inheritdoc/>
        public Task<Result<NodeModelUpdate>> AddToGroup(AddNodeToGroup request) =>
            Handle("node:add-to-group", request);

        /// <inheritdoc/>
        public Task<Result<NodeModelUpdate>> RemoveFromGroup(RemoveNodeFromGroup request) =>
            Handle("node:remove-from-group", request);
    }
}