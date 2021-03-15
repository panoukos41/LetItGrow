using LetItGrow.Microservice.Data.Irrigations.Models;
using MediatR;
using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Irrigations.Requests
{
    public record CreateIrrigation : IRequest
    {
        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        [JsonPropertyName("issuedAt")]
        public Instant IssuedAt { get; init; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        [JsonPropertyName("type")]
        public IrrigationType Type { get; init; }
    }
}