using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to delete a node using its id and concurrency stamp to validate the deletation.
    /// </summary>
    public record DeleteNode : IRequest<Unit>
    {
        /// <summary>
        /// The id of the node to be deleted.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// A random value that must change whenever an object persisted to the store.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

        public DeleteNode()
        {
        }

        public DeleteNode(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }
    }
}