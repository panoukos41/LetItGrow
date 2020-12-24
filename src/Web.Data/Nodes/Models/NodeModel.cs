using LetItGrow.Web.Data.NodeAuths.Models;
using LetItGrow.Web.Data.NodeGroups.Models;
using LetItGrow.Web.Data.Nodes.Json;
using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Web.Data.Nodes.Models
{
    /// <summary>
    /// This is the base record for all nodes.
    /// </summary>
    [JsonConverter(typeof(NodeModelJsonConverter))]
    public abstract record NodeModel
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
        public uint ConcurrencyStamp { get; init; }

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
        /// The nodes type. You can see more types using <see cref="NodeTypes"/>.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; init; } = string.Empty;

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
        public string? NodeGroupId { get; init; }

        /// <summary>
        /// The group that corresponds to the Id property.
        /// </summary>
        [JsonPropertyName("nodeGroup")]
        public NodeGroupModel? NodeGroup { get; set; }

        /// <summary>
        /// The auth that this node contains.
        /// </summary>
        [JsonPropertyName("nodeAuth")]
        public NodeAuthModel? NodeAuth { get; set; }

        /// <summary>
        /// A value indicating if this node is connected to the server.
        /// </summary>
        [JsonPropertyName("connected")]
        public bool Connected { get; set; }
    }
}