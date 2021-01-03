using LetItGrow.Microservice.Data.NodeGroups.Models;
using MediatR;

namespace LetItGrow.Microservice.Data.NodeGroups.Requests
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

        public CreateNodeGroup()
        {
        }

        public CreateNodeGroup(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }
    }
}