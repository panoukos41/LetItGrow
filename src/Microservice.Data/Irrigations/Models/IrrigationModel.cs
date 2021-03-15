using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Irrigations.Models
{
    public record IrrigationModel
    {
        /// <summary>
        /// The id of the irrigation.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The momment this command was issued. This comes from the client.
        /// </summary>
        [JsonPropertyName("issuedAt")]
        public Instant IssuedAt { get; init; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        [JsonPropertyName("type")]
        public IrrigationType Type { get; init; }

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public Instant CreatedAt { get; init; }
    }
}