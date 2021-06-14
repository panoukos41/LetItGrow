using CouchDB.Driver;
using CouchDB.Driver.ChangesFeed;
using LetItGrow.Microservice.Entities;
using LetItGrow.Microservice.Group.Notifications;
using LetItGrow.Microservice.Stores.Internal;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Workers
{
    using Group = Entities.Group;

    public class CouchDbGroupChangesWorker : BackgroundService
    {
        private readonly ICouchDatabase<Group> db;
        private readonly IPublisher publisher;

        public CouchDbGroupChangesWorker(ICouchClient client, IPublisher publisher, IConfiguration configuration)
        {
            var dbName = configuration.GetCouchDbName() ?? "letitgrow";
            db = client.GetDatabase<Group>(dbName, Discriminators.Group);

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
                filter: ChangesFeedFilter.View($"{Views.Group.Design}/{Views.Group.Value}"),
                stoppingToken);

            await foreach (var change in changes)
            {
                var group = change.Document;

                if (change.Deleted)
                {
                    publisher.PublishAndForget(new GroupDeleted(group.Id, group.Rev));
                }
                else if (GetRev(group) == 1)
                {
                    publisher.PublishAndForget(new GroupCreated(group.ToModel()));
                }
                else
                {
                    publisher.PublishAndForget(new GroupUpdated(group.ToModel()));
                }
            }
        }

        private static int GetRev(Group group) =>
            int.Parse(group.Rev.Split('-')[0]);
    }
}