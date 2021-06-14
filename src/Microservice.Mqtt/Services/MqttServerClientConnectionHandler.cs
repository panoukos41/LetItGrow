using CouchDB.Driver;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Services;
using LetItGrow.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Mqtt.Services
{
    using Node = Entities.Node;

    public class MqttServerConnectionHandler : IMqttServerClientConnectedHandler, IMqttServerClientDisconnectedHandler, INodeConnections
    {
        private static readonly ConcurrentDictionary<string, byte?> ConnectedNodes = new();

        private readonly IMqttServer server;
        private readonly IClockService clock;
        private readonly ILogger<MqttServerConnectionHandler> logger;
        private readonly IPublisher publisher;
        private readonly ICouchDatabase<Node> db;

        public MqttServerConnectionHandler(
            IMqttServer server,
            IClockService clock,
            ILogger<MqttServerConnectionHandler> logger,
            IPublisher publisher,
            ICouchClient couch,
            IConfiguration configuration)
        {
            this.server = server;
            this.clock = clock;
            this.logger = logger;
            this.publisher = publisher;
            db = couch.GetDatabase<Node>(configuration.GetCouchDbName() ?? "letitgrow", Discriminators.Node);
        }

        public string[] GetConnectedIds() => ConnectedNodes.Keys.ToArray();

        public async Task HandleClientConnectedAsync(MqttServerClientConnectedEventArgs eventArgs)
        {
            var id = eventArgs.ClientId;
            var node = await db.FindAsync(id) ?? throw new ErrorException(Errors.NotFound);

            publisher.PublishAndForget(new NodeConnection(node.Id, true, clock.GetNow()));
            publisher.PublishAndForget(new NodeSettingsUpdated(node.Id, node.Type, node.Settings.ToJsonDocument()));

            logger.LogInformation("Client '{id}' connected", id);
            await PublishAsync(id, true);
            ConnectedNodes.TryAdd(id, null);
        }

        public async Task HandleClientDisconnectedAsync(MqttServerClientDisconnectedEventArgs eventArgs)
        {
            var id = eventArgs.ClientId;
            var node = await db.FindAsync(id) ?? throw new ErrorException(Errors.NotFound);

            publisher.PublishAndForget(new NodeConnection(node.Id, false, clock.GetNow()));

            logger.LogInformation("Client '{id}' disconnected", id);
            await PublishAsync(id, false);
            ConnectedNodes.TryRemove(id, out _);
        }

        private Task PublishAsync(string clientId, bool connected) => server
            .PublishAsync(new MqttApplicationMessageBuilder()
            .WithAtLeastOnceQoS()
            .WithTopic($"node/connection/{clientId}")
            .WithContentType(ContentTypes.Json)
            .WithPayload(System.Text.Json.JsonSerializer.Serialize(new NodeConnection(clientId, connected, clock.GetNow())))
            .WithRetainFlag()
            .WithDupFlag(ConnectedNodes.ContainsKey(clientId))
            .WithMessageExpiryInterval((uint)TimeSpan.FromDays(1).TotalSeconds)
            .Build());
    }
}