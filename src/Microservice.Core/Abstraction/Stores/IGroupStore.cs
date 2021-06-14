using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Abstraction.Stores
{
    public interface IGroupStore
    {
        ValueTask<GroupModel[]> Search(SearchGroups request, CancellationToken cancellationToken);

        ValueTask<GroupModel?> Find(string id, CancellationToken cancellationToken);

        Task<GroupModel> CreateAsync(CreateGroup request, CancellationToken cancellationToken);

        Task<GroupModel> UpdateAsync(UpdateGroup request, CancellationToken cancellationToken);

        Task DeleteAsync(DeleteGroup request, CancellationToken cancellationToken);
    }
}