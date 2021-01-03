using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeAuths.Requests
{
    public record InvalidateNodeAuth : IRequest
    {
        public string NodeId { get; init; } = string.Empty;

        public InvalidateNodeAuth()
        {
        }

        public InvalidateNodeAuth(string nodeId)
        {
            NodeId = nodeId;
        }

        public InvalidateNodeAuth(NodeModel node)
        {
            NodeId = node.Id;
        }
    }
}