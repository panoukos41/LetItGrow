using CouchDB.Driver;
using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.NotificationHandlers
{
    using Node = Entities.Node;

    public class NodeConnectionHandler : INotificationHandler<NodeConnection>
    {
        private readonly ICouchDatabase<Node> db;

        public NodeConnectionHandler(ICouchClient couch, IConfiguration configuration)
        {
            db = couch.GetDatabase<Node>(configuration.GetCouchDbName() ?? "letitgrow", Discriminators.Node);
        }

        public async Task Handle(NodeConnection notification, CancellationToken cancellationToken)
        {
            var node = await db.FindAsync(notification.NodeId, cancellationToken: cancellationToken)
                ?? throw new ErrorException(Errors.NotFound);

            node.Connected = notification.Status;

            await db.AddOrUpdateAsync(node, cancellationToken: cancellationToken);
        }
    }
}