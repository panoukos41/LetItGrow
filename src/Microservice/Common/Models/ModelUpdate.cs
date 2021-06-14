using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Common.Models
{
    public record ModelUpdate
    {
        /// <summary>
        /// The id of the entity that was updated.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The new ConcurrencyStamp the entity should use.
        /// </summary>
        [JsonPropertyName("concurrency_stamp")]
        public string ConcurrencyStamp { get; set; } = string.Empty;
    }
}