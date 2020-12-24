using LetItGrow.Web.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Web.Data.Nodes.Requests
{
    /// <summary>
    /// Remove a node from a group.
    /// </summary>
    public record RemoveNodeFromGroup : IRequest<NodeModelUpdate>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The nodes concurrency stamp.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }
    }
}