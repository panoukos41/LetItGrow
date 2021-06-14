using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Server;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.NotificationHandlers
{
    public class NodeSettingsUpdatedHandler : INotificationHandler<NodeSettingsUpdated>
    {
        private readonly IMqttServer mqtt;
        private readonly IMemoryCache cache;

        public NodeSettingsUpdatedHandler(IMqttServer mqtt, IMemoryCache cache)
        {
            this.mqtt = mqtt;
            this.cache = cache;
        }

        public async Task Handle(NodeSettingsUpdated notification, CancellationToken cancellationToken)
        {
            var key = $"node-settings-hash:{notification.Id}";
            var hashToCompare = notification.Settings.GetJsonHashCode();

            if (cache.TryGetValue(key, out int hash)
                && hashToCompare == hash)
                return;

            cache.Set(key, hashToCompare, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(14)
            });

            await PublishAsync(notification.Id, notification.Type, notification.Settings);
        }

        private Task PublishAsync(string clientId, NodeType type, JsonDocument settings) => mqtt.PublishAsync(b => b
            .WithAtLeastOnceQoS()
            .WithTopic($"node/settings/{clientId}")
            .WithContentType(ContentTypes.Proto)
            .WithProtoPayload(type switch
            {
                NodeType.Irrigation => ProtoSerializer.Serialize<IrrigationSettings>(settings),
                NodeType.Measurement => ProtoSerializer.Serialize<MeasurementSettings>(settings),
                _ => throw new ArgumentException("Unknown node type at NodeSettingsUpdatedHander", nameof(type))
            })
            .WithRetainFlag(true));
    }
}