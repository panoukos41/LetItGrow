using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LetItGrow.Microservice.Common;

namespace LetItGrow.Microservice.Group.Handlers
{
    public class FindGroupHandler : IRequestHandler<FindGroup, GroupModel>
    {
        private readonly IGroupStore groupStore;

        public FindGroupHandler(IGroupStore groupStore)
        {
            this.groupStore = groupStore;
        }

        public async Task<GroupModel> Handle(FindGroup request, CancellationToken cancellationToken)
        {
            return (await groupStore.Find(request.Id, cancellationToken))
                ?? throw new ErrorException(Errors.NotFound);
        }
    }
}