using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.RestApi.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.NotificationHandlers.NodeHandlers
{
    /// <summary>
    /// Handles Node notifications such as:<br/>
    /// <see cref="NodeCreated"/><br/>
    /// <see cref="NodeUpdated"/><br/>
    /// <see cref="NodeDeleted"/><br/>
    /// <see cref="NodeConnection"/><br/>
    /// </summary>
    public class NodeNotificationHandler :
        INotificationHandler<NodeCreated>,
        INotificationHandler<NodeUpdated>,
        INotificationHandler<NodeDeleted>,
        INotificationHandler<NodeConnection>
    {
        private readonly IHubContext<ApiHub> hub;
        private readonly IMemoryCache cache;

        public NodeNotificationHandler(IHubContext<ApiHub> hub, IMemoryCache cache)
        {
            this.hub = hub;
            this.cache = cache;
        }

        public Task Handle(NodeCreated notification, CancellationToken cancellationToken)
        {
            return ShouldSend($"node:added:{notification.Node.Id}", notification.Node.ConcurrencyStamp)
                ? hub.Clients.All.SendAsync("node:added", notification, cancellationToken)
                : Task.CompletedTask;
        }

        public Task Handle(NodeUpdated notification, CancellationToken cancellationToken)
        {
            return ShouldSend($"node:update:{notification.Node.Id}", notification.Node.ConcurrencyStamp)
                ? hub.Clients.All.SendAsync("node:updated", notification, cancellationToken)
                : Task.CompletedTask;
        }

        public Task Handle(NodeDeleted notification, CancellationToken cancellationToken)
        {
            return ShouldSend($"node:delete:{notification.NodeId}", notification.Rev)
                ? hub.Clients.All.SendAsync("node:removed", notification, cancellationToken)
                : Task.CompletedTask;
        }

        public Task Handle(NodeConnection notification, CancellationToken cancellationToken)
        {
            // todo: Improve node notifications, move to Microservice
            return hub.Clients.All.SendAsync("node:connection", notification.NodeId, notification.Status, notification.At, cancellationToken);
        }

        private bool ShouldSend(string key, string revToCompare)
        {
            if (cache.TryGetValue(key, out string rev))
                return rev != revToCompare;

            cache.Set(key, revToCompare, TimeSpan.FromMinutes(10));
            return true;
        }
    }
}