using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class CreateNodeHandler : IRequestHandler<CreateNode, NodeModel>
    {
        private readonly INodeStore nodeStore;
        private readonly IPublisher publisher;

        public CreateNodeHandler(INodeStore nodeStore, IPublisher publisher)
        {
            this.nodeStore = nodeStore;
            this.publisher = publisher;
        }

        public async Task<NodeModel> Handle(CreateNode request, CancellationToken cancellationToken)
        {
            var node = await nodeStore.CreateAsync(request, cancellationToken);

            publisher.PublishAndForget(new NodeCreated(node));

            return node;
        }
    }
}