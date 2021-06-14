using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using MQTTnet.Server;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.NotificationHandlers
{
    public class DisconnectNodeHandler : INotificationHandler<DisconnectNode>
    {
        private readonly IMqttServer mqtt;

        public DisconnectNodeHandler(IMqttServer mqtt)
        {
            this.mqtt = mqtt;
        }

        public Task Handle(DisconnectNode notification, CancellationToken cancellationToken)
        {
            DisconnectNode(notification.NodeId);

            return Task.CompletedTask;
        }

        private void DisconnectNode(string nodeId) => Observable
            .FromAsync(mqtt.GetClientStatusAsync)
            .Select(x => x.FirstOrDefault(status => status.ClientId == nodeId))
            .Where(x => x is not null)
            .Subscribe(status => Observable.FromAsync(status!.DisconnectAsync).Subscribe());
    }
}