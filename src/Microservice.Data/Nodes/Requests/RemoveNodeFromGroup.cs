using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
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