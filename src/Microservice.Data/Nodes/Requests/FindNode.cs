using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to get a single node using its id.
    /// </summary>
    public record FindNode : IRequest<NodeModel>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        public FindNode()
        {
        }

        public FindNode(string id)
        {
            Id = id;
        }
    }
}