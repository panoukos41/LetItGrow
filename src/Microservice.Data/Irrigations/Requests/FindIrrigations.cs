using LetItGrow.Microservice.Data.Irrigations.Models;
using MediatR;
using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Irrigations.Requests
{
    public record FindIrrigations : IRequest<IrrigationModel[]>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("from")]
        public Instant From { get; set; }

        [JsonPropertyName("to")]
        public Instant To { get; set; }
    }
}