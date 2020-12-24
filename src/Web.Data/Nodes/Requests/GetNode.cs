using LetItGrow.Web.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Web.Data.Nodes.Requests
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
    }
}