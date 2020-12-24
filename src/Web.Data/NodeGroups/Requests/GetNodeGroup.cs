using LetItGrow.Web.Data.NodeGroups.Models;
using MediatR;

namespace LetItGrow.Web.Data.NodeGroups.Requests
{
    /// <summary>
    /// A request to get a node group.
    /// </summary>
    public record GetNodeGroup : IRequest<NodeGroupModel>
    {
        /// <summary>
        /// The id of the group
        /// </summary>
        public string Id { get; init; } = string.Empty;
    }
}