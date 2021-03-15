using LetItGrow.Microservice.Data.Measurements.Models;
using MediatR;
using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Measurements.Requests
{
    public record FindMeasurements : IRequest<MeasurementModel[]>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("from")]
        public Instant From { get; set; }

        [JsonPropertyName("to")]
        public Instant To { get; set; }
    }
}