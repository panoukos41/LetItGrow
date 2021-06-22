using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Mqtt.Services;
using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.NotificationHandlers
{
    public class NodeAuthenticationHandler :
        INodeTokenAuthenticator,
        INotificationHandler<NodeDeleted>,
        INotificationHandler<NodeTokenChanged>
    {
        private readonly INodeStore store;
        private readonly IMemoryCache cache;
        private readonly IPublisher publisher;

        public NodeAuthenticationHandler(INodeStore store, IMemoryCache cache, IPublisher publisher)
        {
            this.store = store;
            this.cache = cache;
            this.publisher = publisher;
        }

        public Task Handle(NodeDeleted notification, CancellationToken cancellationToken)
        {
            var token = FindToken(notification.NodeId);

            if (token is { })
            {
                RemoveToken(notification.NodeId);
            }

            publisher.PublishAndForget(new DisconnectNode(notification.NodeId));

            return Task.CompletedTask;
        }

        public Task Handle(NodeTokenChanged notification, CancellationToken cancellationToken)
        {
            var nodeId = notification.NodeId;
            var token = FindToken(nodeId);

            if (token is null)
            {
                SetToken(nodeId, notification.Token);
            }
            else if (token != notification.Token)
            {
                SetToken(nodeId, notification.Token);
                publisher.PublishAndForget(new DisconnectNode(nodeId));
            }

            return Task.CompletedTask;
        }

        public async ValueTask<bool> Authenticate(string nodeId, string token)
        {
            if (FindToken(nodeId) is { } storedToken)
            {
                return storedToken == token;
            }

            var node = await store.Find(nodeId, default);
            if (node is not null)
            {
                SetToken(nodeId, token);

                return node.Token == token;
            }
            return false;
        }

        private string? FindToken(string nodeId) =>
            cache.Get<string>($"node-auth:{nodeId}") is { } token ? token : null;

        private void SetToken(string nodeId, string token, TimeSpan? expiration = null) =>
            cache.Set($"node-auth:{nodeId}", token, new MemoryCacheEntryOptions
            {
                SlidingExpiration = expiration ?? TimeSpan.FromMinutes(30)
            });

        private void RemoveToken(string nodeId) =>
            cache.Remove($"node-auth:{nodeId}");
    }
}