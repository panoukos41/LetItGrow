using LetItGrow.Microservice.Node.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Notifications
{
    public record NodeUpdated : INotification
    {
        [JsonPropertyName("node")]
        public NodeModel Node { get; }

        public NodeUpdated(NodeModel node)
        {
            Node = node;
        }
    }
}