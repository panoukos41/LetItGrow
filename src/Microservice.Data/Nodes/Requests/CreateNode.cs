using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// Base record for anything that has to do with a node request in general.<br/>
    /// This is only meant for internal use to create the public types.
    /// </summary>
    public record CreateNode : IRequest<NodeModel>
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