using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Node.Requests;
using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class UpdateNodeHandler : IRequestHandler<UpdateNode, ModelUpdate>
    {
        private readonly INodeStore nodeStore;
        private readonly IPublisher publisher;

        public UpdateNodeHandler(INodeStore nodeStore, IPublisher publisher)
        {
            this.nodeStore = nodeStore;
            this.publisher = publisher;
        }

        public async Task<ModelUpdate> Handle(UpdateNode request, CancellationToken cancellationToken)
        {
            var node = await nodeStore.UpdateAsync(request, cancellationToken);

            // Send an updated notificcation and execute afterUpdate
            // only when the concurrency stamp is updated.
            if (node.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                publisher.PublishAndForget(new NodeUpdated(node));
            }

            return new ModelUpdate
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp
            };
        }
    }
}