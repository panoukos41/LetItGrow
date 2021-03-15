using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// Remove a node from a group.
    /// </summary>
    public record RemoveNodeFromGroup : IRequest<ModelUpdate>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The nodes concurrency stamp.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

        public RemoveNodeFromGroup()
        {
        }

        public RemoveNodeFromGroup(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }
    }
}