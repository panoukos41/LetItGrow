using LetItGrow.Microservice.Common.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Models
{
    /// <summary>
    /// This is the base record for all nodes.
    /// </summary>
    public record NodeModel : BaseModel
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the node. (optional)
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The node's type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NodeType Type { get; set; }

        /// <summary>
        /// The token that was generated.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// The nodes settings.
        /// </summary>
        [JsonPropertyName("settings")]
        public JsonDocument? Settings { get; set; }

        /// <summary>
        /// The exact momment this node was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The exact momment this node was updated.
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
        /// A flag that indicates if the node is connected or not.
        /// </summary>
        [JsonPropertyName("connected")]
        public bool Connected { get; set; }

        /// <summary>
        /// A foreign key that points to the node's group.
        /// </summary>
        [JsonPropertyName("nodeGroupId")]
        public string? GroupId { get; set; }
    }
}