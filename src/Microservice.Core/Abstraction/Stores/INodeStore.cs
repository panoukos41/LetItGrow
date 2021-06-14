using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Abstraction.Stores
{
    public interface INodeStore
    {
        ValueTask<NodeModel[]> Search(SearchNodes request, CancellationToken cancellationToken);

        ValueTask<NodeModel?> Find(string id, CancellationToken cancellationToken);

        Task<NodeModel> CreateAsync(CreateNode request, CancellationToken cancellationToken);

        Task<NodeModel> UpdateAsync(UpdateNode request, CancellationToken cancellationToken);

        Task DeleteAsync(DeleteNode request, CancellationToken cancellationToken);

        Task<NodeModel> GroupAddAsync(GroupAdd request, CancellationToken cancellationToken);

        Task<NodeModel> GroupRemoveAsyn(GroupRemove request, CancellationToken cancellationToken);

        Task<NodeModel> RefreshToken(RefreshToken request, CancellationToken cancellationToken);
    }
}