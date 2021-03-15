using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Groups.Models
{
    /// <summary>
    /// The model for a node group.
    /// </summary>
    public record GroupModel
    {
        /// <summary>
        /// The group's unique id.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// A value to indicate if the data has changed since last retreived.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public Instant CreatedAt { get; init; }

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public Instant UpdatedAt { get; init; }

        /// <summary>
        /// The id of the user that created this entity.
        /// </summary>
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; init; } = string.Empty;

        /// <summary>
        /// The id of the user that updated this entity.
        /// </summary>
        [JsonPropertyName("updatedBy")]
        public string UpdatedBy { get; init; } = string.Empty;

        /// <summary>
        /// This group's type.
        /// </summary>
        [JsonPropertyName("type")]
        public GroupType Type { get; init; }

        /// <summary>
        /// A name for the group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// A small description for the group.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        public void Deconstruct(out string id, out string name)
        {
            id = Id;
            name = Name;
        }
    }
}