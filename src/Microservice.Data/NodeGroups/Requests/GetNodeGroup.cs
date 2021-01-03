using LetItGrow.Microservice.Data.NodeGroups.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeGroups.Requests
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

        public GetNodeGroup()
        {
        }

        public GetNodeGroup(string id)
        {
            Id = id;
        }
    }
}