using LetItGrow.Microservice.Data.NodeAuths.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.NodeAuths.Requests
{
    public record FindNodeAuth : IRequest<NodeAuthModel>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        public FindNodeAuth()
        {
        }

        public FindNodeAuth(string nodeId)
        {
            NodeId = nodeId;
        }

        public FindNodeAuth(NodeModel node)
        {
            NodeId = node.Id;
        }
    }
}