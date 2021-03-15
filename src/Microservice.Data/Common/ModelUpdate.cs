using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Common
{
    public record ModelUpdate
    {
        /// <summary>
        /// The id of the entity that was updated.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The new ConcurrencyStamp the entity should use.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;
    }
}