using CouchDB.Driver;
using CouchDB.Driver.ChangesFeed;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Stores.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Server;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Workers
{
    using Irrigation = Entities.Irrigation;

    public class CouchDbIrrigationChangesWorker : BackgroundService
    {
        private readonly ICouchDatabase<Irrigation> db;
        private readonly IMqttServer mqtt;
        private readonly IMemoryCache cache;

        public CouchDbIrrigationChangesWorker(
            ICouchClient client,
            IMqttServer mqtt,
            IMemoryCache cache,
            IConfiguration configuration)
        {
            var dbName = configuration.GetCouchDbName() ?? "letitgrow";
            db = client.GetDatabase<Irrigation>(dbName, Discriminators.Irrigation);

            this.mqtt = mqtt;
            this.cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await Task.Delay(1000, stoppingToken).Ignore();

            if (stoppingToken.IsCancellationRequested) return;

            var changes = db.GetContinuousChangesAsync(
                options: new()
                {
                    IncludeDocs = true,
                    Since = "now",
                    Heartbeat = 60_001
                },
                filter: ChangesFeedFilter.View($"{Views.Irrigations.Design}/{Views.Irrigations.Value}"),
                stoppingToken);

            await foreach (var change in changes)
            {
                if (change.Deleted || !RevIsOne(change.Document.Rev)) continue;

                var key = $"irrigation-hash:{change.Document.Id}";

                if (cache.TryGetValue(key, out _)) continue;

                cache.Set(key, byte.MinValue, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                });

                await PublishAsync(change.Document.ToModel());
            }
        }

        private static bool RevIsOne(string rev) =>
            int.Parse(rev.Split('-')[0]) == 1;

        private Task PublishAsync(IrrigationModel irrigation) => mqtt
            .PublishAsync(new MqttApplicationMessageBuilder()
            .WithAtLeastOnceQoS()
            .WithTopic($"node/irrigation/{irrigation.NodeId}")
            .WithProtoPayload(new CreateIrrigation
            {
                NodeId = irrigation.NodeId,
                Type = irrigation.Type,
                IssuedAt = irrigation.IssuedAt
            })
            .WithRetainFlag(true)
            .WithMessageExpiryInterval((uint)TimeSpan.FromDays(1).TotalSeconds)
            .Build());
    }
}