using LetItGrow.Microservice.Data.Common;
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
    public record UpdateNode : IRequest<ModelUpdate>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The nodes concurrency stamp to validate if the node is up to date.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

        /// <summary>
        /// This node's type.
        /// </summary>
        [JsonPropertyName("type")]
        public NodeType Type { get; init; }

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
        }
    }
}