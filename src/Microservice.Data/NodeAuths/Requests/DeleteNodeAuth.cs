using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.NodeAuths.Requests
{
    public record DeleteNodeAuth : IRequest<Unit>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        public DeleteNodeAuth()
        {
        }

        public DeleteNodeAuth(string nodeId)
        {
            NodeId = nodeId;
        }

        public DeleteNodeAuth(NodeModel node)
        {
            NodeId = node.Id;
        }
    }
}