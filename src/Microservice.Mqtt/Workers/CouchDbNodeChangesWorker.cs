﻿using CouchDB.Driver;
using CouchDB.Driver.ChangesFeed;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Stores.Internal;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Workers
{
    using Node = Entities.Node;

    public class CouchDbNodeChangesWorker : BackgroundService
    {
        private readonly ICouchDatabase<Node> db;
        private readonly IPublisher publisher;

        public CouchDbNodeChangesWorker(ICouchClient client, IPublisher publisher, IConfiguration configuration)
        {
            var dbName = configuration.GetCouchDbName() ?? "letitgrow";
            db = client.GetDatabase<Node>(dbName, Discriminators.Node);

            this.publisher = publisher;
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
                filter: ChangesFeedFilter.View($"{Views.Node.Design}/{Views.Node.Value}"),
                stoppingToken);

            await foreach (var change in changes)
            {
                var node = change.Document;

                if (change.Deleted)
                {
                    publisher.PublishAndForget(new NodeDeleted(node.Id, node.Rev));
                    continue;
                }

                publisher.PublishAndForget(new NodeTokenChanged(node.Id, node.Rev, node.Token));
                publisher.PublishAndForget(new NodeSettingsUpdated(node.Id, node.Type, node.Settings.ToJsonDocument()));
            }
        }
    }
}