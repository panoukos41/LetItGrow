using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.NodeAuths.Models
{
    public record NodeAuthModel
    {
        /// <summary>
        /// The id of this authentication.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The id of the node that this auth belongs to.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The token that was generated. This is not null only the first time its created.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public Instant CreatedAt { get; set; }

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public Instant UpdatedAt { get; set; }

        /// <summary>
        /// The id of the user that created this entity.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The id of the user that updated this entity.
        /// </summary>
        [JsonPropertyName("updatedBy")]
        public string UpdatedBy { get; set; } = string.Empty;

        /// <summary>
        /// A random value that must change whenever an object is persisted to the store.<br/>
        /// This is usually generated in the database.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}