using LetItGrow.Microservice.Data.NodeAuths.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeAuths.Requests
{
    public record CreateNodeAuth : IRequest<NodeAuthModel>
    {
        public string NodeId { get; init; } = string.Empty;

        public CreateNodeAuth()
        {
        }

        public CreateNodeAuth(string nodeId)
        {
            NodeId = nodeId;
        }

        public CreateNodeAuth(NodeModel node)
        {
            NodeId = node.Id;
        }
    }
}