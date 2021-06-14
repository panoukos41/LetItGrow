using MediatR;
using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Notifications
{
    public record NodeConnection : INotification
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; }

        [JsonPropertyName("status")]
        public bool Status { get; }

        [JsonPropertyName("at")]
        public DateTimeOffset At { get; }

        public NodeConnection(string id, bool status, DateTimeOffset at)
        {
            NodeId = id;
            Status = status;
            At = at;
        }

        public void Deconstruct(out string id, out bool status)
        {
            id = NodeId;
            status = Status;
        }

        public void Deconstruct(out string id, out bool status, out DateTimeOffset at)
        {
            Deconstruct(out id, out status);
            at = At;
        }
    }
}