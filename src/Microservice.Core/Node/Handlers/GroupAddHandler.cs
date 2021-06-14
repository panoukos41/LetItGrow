using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Node.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class GroupAddHandler : IRequestHandler<GroupAdd, ModelUpdate>
    {
        private readonly INodeStore nodeStore;
        private readonly IPublisher publisher;

        public GroupAddHandler(INodeStore nodeStore, IPublisher publisher)
        {
            this.nodeStore = nodeStore;
            this.publisher = publisher;
        }

        public async Task<ModelUpdate> Handle(GroupAdd request, CancellationToken cancellationToken)
        {
            var node = await nodeStore.GroupAddAsync(request, cancellationToken);

            // Send an updated notificcation only when the concurrency stamp is updated.
            if (node.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                publisher.PublishAndForget(new NodeUpdated(node));
            }

            return new()
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp
            };
        }
    }
}