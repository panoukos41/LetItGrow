using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class SearchNodesHandler : IRequestHandler<SearchNodes, NodeModel[]>
    {
        private readonly INodeStore nodeStore;

        public SearchNodesHandler(INodeStore nodeStore)
        {
            this.nodeStore = nodeStore;
        }

        public Task<NodeModel[]> Handle(SearchNodes request, CancellationToken cancellationToken)
        {
            return nodeStore.Search(request, cancellationToken).AsTask();
        }
    }
}