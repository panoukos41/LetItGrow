using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to get a single node using its id.
    /// </summary>
    public record GetNode : IRequest<NodeModel>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        public GetNode()
        {
        }

        public GetNode(string id)
        {
            Id = id;
        }
    }
}