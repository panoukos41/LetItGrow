using CouchDB.Driver;
using CouchDB.Driver.ChangesFeed;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Stores.Internal;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
                }
                else if (GetRev(node) == 1)
                {
                    publisher.PublishAndForget(new NodeCreated(node.ToModel()));
                }
                else
                {
                    publisher.PublishAndForget(new NodeUpdated(node.ToModel()));
                }
            }
        }

        private static int GetRev(Node node) =>
            int.Parse(node.Rev.Split('-')[0]);
    }
}