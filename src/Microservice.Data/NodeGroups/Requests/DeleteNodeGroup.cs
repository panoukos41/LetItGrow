using LetItGrow.Microservice.Data.NodeGroups.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeGroups.Requests
{
    /// <summary>
    /// A request to delete a node group.
    /// </summary>
    public record DeleteNodeGroup : IRequest
    {
        /// <summary>
        /// The id of the group.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the group.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }

        public DeleteNodeGroup()
        {

        }

        public DeleteNodeGroup(NodeGroupModel group)
        {
            Id = group.Id;
            ConcurrencyStamp = group.ConcurrencyStamp;
        }
    }
}