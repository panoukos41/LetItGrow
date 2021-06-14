using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Notifications;
using LetItGrow.Microservice.Node.Requests;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshToken, RefreshModel>
    {
        private readonly INodeStore nodeStore;
        private readonly IPublisher publisher;

        public RefreshTokenHandler(INodeStore nodeStore, IPublisher publisher)
        {
            this.nodeStore = nodeStore;
            this.publisher = publisher;
        }

        public async Task<RefreshModel> Handle(RefreshToken request, CancellationToken cancellationToken)
        {
            var node = await nodeStore.RefreshToken(request, cancellationToken);

            // Send an updated notificcation and execute afterUpdate
            // only when the concurrency stamp is updated.
            if (node.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                publisher.PublishAndForget(new NodeUpdated(node));
            }

            return new RefreshModel
            {
                Id = node.Id,
                ConcurrencyStamp = node.ConcurrencyStamp,
                Token = node.Token
            };
        }
    }
}