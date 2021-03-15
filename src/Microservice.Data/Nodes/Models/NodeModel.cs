using NodaTime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Models
{
    /// <summary>
    /// This is the base record for all nodes.
    /// </summary>
    public record NodeModel
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// A random value that must change whenever an object persisted to the store.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

        /// <summary>
        /// The name of the node.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// The description of the node. (optional)
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; init; }

        /// <summary>
        /// The node's type.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public NodeType Type { get; init; }

        [JsonPropertyName("settings")]
        public JsonDocument? Settings { get; set; }

        /// <summary>
        /// The exact momment this node was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public Instant CreatedAt { get; init; }

        /// <summary>
        /// The exact momment this node was updated.
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
        /// A foreign key that points to the node's group.
        /// </summary>
        [JsonPropertyName("nodeGroupId")]
        public string? GroupId { get; init; }

        /// <summary>
        /// A value indicating if this node is connected to the server.
        /// </summary>
        [JsonPropertyName("connected")]
        public bool Connected { get; set; }
    }
}