using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.Microservice.Abstraction.Stores;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LetItGrow.Microservice.Common;

namespace LetItGrow.Microservice.Node.Handlers
{
    public class FindNodeHandler : IRequestHandler<FindNode, NodeModel>
    {
        private readonly INodeStore nodeStore;

        public FindNodeHandler(INodeStore nodeStore)
        {
            this.nodeStore = nodeStore;
        }

        public async Task<NodeModel> Handle(FindNode request, CancellationToken cancellationToken)
        {
            return (await nodeStore.Find(request.Id, cancellationToken))
                ?? throw new ErrorException(Errors.NotFound);
        }
    }
}