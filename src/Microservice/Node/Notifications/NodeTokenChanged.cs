using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Notifications
{
    public record NodeTokenChanged : INotification
    {
        public NodeTokenChanged(string nodeId, string rev, string token)
        {
            NodeId = nodeId;
            Rev = rev;
            Token = token;
        }

        [JsonPropertyName("nodeId")]
        public string NodeId { get; }

        [JsonPropertyName("rev")]
        public string Rev { get; }

        [JsonPropertyName("token")]
        public string Token { get; }
    }
}