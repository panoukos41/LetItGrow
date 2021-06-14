using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class DeleteNodeHandler : IRequestHandler<DeleteNode, Unit>
    {
        private readonly INodeStore nodeStore;
        private readonly IPublisher publisher;

        public DeleteNodeHandler(INodeStore nodeStore, IPublisher publisher)
        {
            this.nodeStore = nodeStore;
            this.publisher = publisher;
        }

        public async Task<Unit> Handle(DeleteNode request, CancellationToken cancellationToken)
        {
            await nodeStore.DeleteAsync(request, cancellationToken);

            // Send a deleted notificcation.
            publisher.PublishAndForget(new NodeDeleted(request.Id, request.ConcurrencyStamp));

            return Unit.Value;
        }
    }
}