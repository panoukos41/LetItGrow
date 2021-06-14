using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Group.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Group.Handlers
{
    public class CreateGroupHandler : IRequestHandler<CreateGroup, GroupModel>
    {
        private readonly IGroupStore groupStore;
        private readonly IPublisher publisher;

        public CreateGroupHandler(IGroupStore groupStore, IPublisher publisher)
        {
            this.groupStore = groupStore;
            this.publisher = publisher;
        }

        public async Task<GroupModel> Handle(CreateGroup request, CancellationToken cancellationToken)
        {
            var group = await groupStore.CreateAsync(request, cancellationToken);

            publisher.PublishAndForget(new GroupCreated(group));

            return group;
        }
    }
}