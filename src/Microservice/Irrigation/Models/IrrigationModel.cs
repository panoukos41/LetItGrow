using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Irrigation.Models
{
    public record IrrigationModel
    {
        /// <summary>
        /// The id of the irrigation.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        /// <summary>
        /// The momment this command was issued. This comes from the client.
        /// </summary>
        [JsonPropertyName("issuedAt")]
        public DateTimeOffset IssuedAt { get; set; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        [JsonPropertyName("type"), JsonConverter(typeof(JsonStringEnumConverter))]
        public IrrigationType Type { get; set; }

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }
    }
}