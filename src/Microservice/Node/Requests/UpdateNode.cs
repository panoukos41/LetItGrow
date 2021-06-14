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
    public record UpdateNode : BaseUpdate
    {
        /// <summary>
        /// This node's type.
        /// </summary>
        [JsonPropertyName("type")]
        public NodeType Type { get; set; }

        /// <summary>
        /// A new name for the node.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// A new description for the node.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// This node's new settings.
        /// </summary>
        [JsonPropertyName("settings")]
        public JsonDocument? Settings { get; set; }

        public UpdateNode()
        {
        }

        /// <summary>
        /// Create a new request and fill the node's details (Id, ConcurrencyStamp, Type)
        /// </summary>
        /// <param name="node"></param>
        public UpdateNode(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
            Type = node.Type;
            Settings = node.Settings;
        }
    }
}