using LetItGrow.Microservice.Data.NodeAuths.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeAuths.Requests
{
    public record GetNodeAuth : IRequest<NodeAuthModel>
    {
        public string NodeId { get; init; } = string.Empty;

        public GetNodeAuth()
        {
        }

        public GetNodeAuth(string nodeId)
        {
            NodeId = nodeId;
        }

        public GetNodeAuth(NodeModel node)
        {
            NodeId = node.Id;
        }
    }
}