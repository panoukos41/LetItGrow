using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Group.Notifications;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LetItGrow.Microservice.Common.Models;

namespace LetItGrow.Microservice.Group.Handlers
{
    public class UpdateGroupHandler : IRequestHandler<UpdateGroup, ModelUpdate>
    {
        private readonly IGroupStore groupStore;
        private readonly IPublisher publisher;

        public UpdateGroupHandler(IGroupStore groupStore, IPublisher publisher)
        {
            this.groupStore = groupStore;
            this.publisher = publisher;
        }

        public async Task<ModelUpdate> Handle(UpdateGroup request, CancellationToken cancellationToken)
        {
            var group = await groupStore.UpdateAsync(request, cancellationToken);

            if (group.ConcurrencyStamp != request.ConcurrencyStamp)
            {
                publisher.PublishAndForget(new GroupUpdated(group));
            }

            return new ModelUpdate
            {
                Id = group.Id,
                ConcurrencyStamp = group.ConcurrencyStamp
            };
        }
    }
}