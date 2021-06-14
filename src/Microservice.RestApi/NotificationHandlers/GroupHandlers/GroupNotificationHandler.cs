using LetItGrow.Microservice.Group.Notifications;
using LetItGrow.Microservice.RestApi.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.NotificationHandlers.GroupHandlers
{
    /// <summary>
    /// Handles NodeGroup notifications such as:<br/>
    /// <see cref="GroupCreated"/><br/>
    /// <see cref="GroupUpdated"/><br/>
    /// <see cref="GroupDeleted"/><br/>
    /// </summary>
    public class GroupNotificationHandler :
        INotificationHandler<GroupCreated>,
        INotificationHandler<GroupUpdated>,
        INotificationHandler<GroupDeleted>
    {
        private readonly IHubContext<ApiHub> hub;
        private readonly IMemoryCache cache;

        public GroupNotificationHandler(IHubContext<ApiHub> hub, IMemoryCache cache)
        {
            this.hub = hub;
            this.cache = cache;
        }

        public Task Handle(GroupCreated notification, CancellationToken cancellationToken)
        {
            return ShouldSend($"group:create:{notification.Group.Id}", notification.Group.ConcurrencyStamp)
                ? hub.Clients.All.SendAsync("group:added", notification, cancellationToken)
                : Task.CompletedTask;
        }

        public Task Handle(GroupUpdated notification, CancellationToken cancellationToken)
        {
            return ShouldSend($"group:update:{notification.Group.Id}", notification.Group.ConcurrencyStamp)
                ? hub.Clients.All.SendAsync("group:updated", notification, cancellationToken)
                : Task.CompletedTask;
        }

        public Task Handle(GroupDeleted notification, CancellationToken cancellationToken)
        {
            return ShouldSend($"group:create:{notification.GroupId}", notification.Rev)
                ? hub.Clients.All.SendAsync("group:removed", notification, cancellationToken)
                : Task.CompletedTask;
        }

        private bool ShouldSend(string key, string revToCompare)
        {
            if (cache.TryGetValue(key, out string rev))
                return rev != revToCompare;

            cache.Set(key, revToCompare, TimeSpan.FromMinutes(10));
            return false;
        }
    }
}