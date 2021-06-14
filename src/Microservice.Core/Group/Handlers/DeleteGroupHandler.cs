using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Group.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Group.Handlers
{
    public class DeleteGroupHandler : IRequestHandler<DeleteGroup, Unit>
    {
        private readonly IGroupStore groupStore;
        private readonly IPublisher publisher;

        public DeleteGroupHandler(IGroupStore groupStore, IPublisher publisher)
        {
            this.groupStore = groupStore;
            this.publisher = publisher;
        }

        public async Task<Unit> Handle(DeleteGroup request, CancellationToken cancellationToken)
        {
            await groupStore.DeleteAsync(request, cancellationToken);

            publisher.PublishAndForget(new GroupDeleted(request.Id, request.ConcurrencyStamp));

            return Unit.Value;
        }
    }
}