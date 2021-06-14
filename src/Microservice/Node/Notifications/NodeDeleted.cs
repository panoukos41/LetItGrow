using LetItGrow.Microservice.Node.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Notifications
{
    public record NodeDeleted : INotification
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; }

        [JsonPropertyName("rev")]
        public string Rev { get; }

        public NodeDeleted(string id, string rev)
        {
            NodeId = id;
            Rev = rev;
        }

        public NodeDeleted(NodeModel node)
        {
            NodeId = node.Id;
            Rev = node.ConcurrencyStamp;
        }
    }
}