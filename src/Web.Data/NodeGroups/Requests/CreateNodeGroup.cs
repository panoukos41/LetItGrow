using LetItGrow.Web.Data.NodeGroups.Models;
using MediatR;

namespace LetItGrow.Web.Data.NodeGroups.Requests
{
    /// <summary>
    /// A request to create a new node group.
    /// </summary>
    public record CreateNodeGroup : IRequest<NodeGroupModel>
    {
        /// <summary>
        /// A name for the group.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// A small description for the group. (optional)
        /// </summary>
        public string? Description { get; init; }
    }
}