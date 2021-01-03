using LetItGrow.Microservice.Data.NodeGroups.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeGroups.Requests
{
    /// <summary>
    /// A request to update a node group.
    /// </summary>
    public record UpdateNodeGroup : IRequest<NodeGroupModelUpdate>
    {
        /// <summary>
        /// The id of the group.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the group.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }

        /// <summary>
        /// A new name for the group. if it's null it won't update.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// A new description for the group. If it's null it won't update.
        /// </summary>
        public string? Description { get; init; }

        public UpdateNodeGroup()
        {
        }

        public UpdateNodeGroup(NodeGroupModel group)
        {
            Id = group.Id;
            ConcurrencyStamp = group.ConcurrencyStamp;
        }
    }
}