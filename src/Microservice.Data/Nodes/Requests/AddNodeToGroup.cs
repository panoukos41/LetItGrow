using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to add a node to a group.
    /// </summary>
    public record AddNodeToGroup : IRequest<NodeModelUpdate>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the node.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }

        /// <summary>
        /// The id of the group that the node will be added to.
        /// </summary>
        public string GroupId { get; init; } = string.Empty;

        public AddNodeToGroup()
        {
        }

        public AddNodeToGroup(NodeModel node, NodeGroupModel group)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
            GroupId = group.Id;
        }
    }
}