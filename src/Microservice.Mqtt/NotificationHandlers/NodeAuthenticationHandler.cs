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

        public NodeAuthenticationHandler(INodeStore store, IMemoryCache cache)
        {
            this.store = store;
            this.cache = cache;
        }

        private record Auth(int Rev, string Token);

        public Task Handle(NodeDeleted notification, CancellationToken cancellationToken)
        {
            var auth = FindAuth(notification.NodeId);

            if (auth is { } && auth.Rev <= GetRev(notification.Rev))
            {
                RemoveAuth(notification.NodeId);
            }

            return Task.CompletedTask;
        }

        public Task Handle(NodeTokenChanged notification, CancellationToken cancellationToken)
        {
            var rev = GetRev(notification.Rev);
            var nodeId = notification.NodeId;
            var auth = FindAuth(nodeId);

            if (auth is null)
            {
                SetAuth(nodeId, new Auth(rev, notification.Token));
            }
            else if (auth.Rev < rev)
            {
                SetAuth(nodeId, auth with
                {
                    Rev = rev,
                    Token = notification.Token
                });
            }
            return Task.CompletedTask;
        }

        public async ValueTask<bool> Authenticate(string nodeId, string token)
        {
            if (FindAuth(nodeId) is { } auth)
            {
                return auth.Token == token;
            }

            var node = await store.Find(nodeId, default);
            if (node is not null)
            {
                SetAuth(nodeId, new Auth(GetRev(node.ConcurrencyStamp), token));

                return node.Token == token;
            }

            SetAuth(nodeId, new Auth(1, token));
            return false;
        }

        private Auth? FindAuth(string nodeId) =>
            cache.Get<Auth>($"node-auth:{nodeId}") is { } auth ? auth : null;

        private void SetAuth(string nodeId, Auth auth, TimeSpan? expiration = null) =>
            cache.Set($"node-auth:{nodeId}", auth, new MemoryCacheEntryOptions
            {
                SlidingExpiration = expiration ?? TimeSpan.FromMinutes(30)
            });

        private void RemoveAuth(string nodeId) =>
            cache.Remove($"node-auth:{nodeId}");

        private static int GetRev(string rev) =>
            int.Parse(rev.Split('-')[0]);
    }
}