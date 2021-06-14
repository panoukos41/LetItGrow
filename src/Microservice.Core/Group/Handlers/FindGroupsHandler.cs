using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Group.Handlers
{
    public class FindGroupsHandler : IRequestHandler<SearchGroups, GroupModel[]>
    {
        private readonly IGroupStore groupStore;

        public FindGroupsHandler(IGroupStore groupStore)
        {
            this.groupStore = groupStore;
        }

        public Task<GroupModel[]> Handle(SearchGroups request, CancellationToken cancellationToken)
        {
            return groupStore.Search(request, cancellationToken).AsTask();
        }
    }
}