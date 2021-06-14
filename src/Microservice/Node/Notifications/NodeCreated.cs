using LetItGrow.Microservice.Node.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Notifications
{
    public record NodeCreated : INotification
    {
        [JsonPropertyName("node")]
        public NodeModel Node { get; }

        public NodeCreated(NodeModel node)
        {
            Node = node;
        }
    }
}