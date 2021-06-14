using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Common.Behaviours
{
    public class NotificationLogger<TNotification> : INotificationHandler<TNotification> where TNotification : INotification
    {
        private readonly ILogger<TNotification> logger;

        public NotificationLogger(ILogger<TNotification> logger)
        {
            this.logger = logger;
        }

        public Task Handle(TNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Notification {notification}", notification);

            return Task.CompletedTask;
        }
    }
}