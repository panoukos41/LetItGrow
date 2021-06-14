using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Node.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LetItGrow.Microservice.Common.Models;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class GroupRemoveHandler : IRequestHandler<GroupRemove, ModelUpdate>
    {
        private readonly INodeStore nodeStore;
        private readonly IPublisher publisher;

        public GroupRemoveHandler(INodeStore nodeStore, IPublisher publisher)
        {
            this.nodeStore = nodeStore;
            this.publisher = publisher;
        }

        public async Task<ModelUpdate> Handle(GroupRemove request, CancellationToken cancellationToken)
        {
            var node = await nodeStore.GroupRemoveAsyn(request, cancellationToken).ConfigureAwait(false);

            // Send an updated notificcation only when the concurrency stamp is updated.
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