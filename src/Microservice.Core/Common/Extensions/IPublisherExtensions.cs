using System.Threading.Tasks;

namespace MediatR
{
    public static class IPublisherExtensions
    {
        public static void PublishAndForget(this IPublisher publisher, INotification notification)
        {
            Task.Run(() => publisher.Publish(notification));
        }
    }
}