using LetItGrow.Microservice.Common.Models;
using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Group.Models
{
    /// <summary>
    /// The model for a node group.
    /// </summary>
    public record GroupModel : BaseModel
    {
        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        [JsonPropertyName("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

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
        /// This group's type.
        /// </summary>
        [JsonPropertyName("type")]
        public GroupType Type { get; set; }

        /// <summary>
        /// A name for the group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A small description for the group.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        public void Deconstruct(out string id, out string name)
        {
            id = Id;
            name = Name;
        }
    }
}