using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Node.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Requests
{
    /// <summary>
    /// Base record for anything that has to do with a node request in general.<br/>
    /// This is only meant for internal use to create the public types.
    /// </summary>
    public record CreateNode : BaseCreate<NodeModel>
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
        /// The type of node to create.
        /// </summary>
        [JsonPropertyName("type")]
        public NodeType Type { get; set; }

        /// <summary>
        /// The settings for a new node.
        /// </summary>
        [JsonPropertyName("settings")]
        public JsonDocument? Settings { get; set; }
    }
}