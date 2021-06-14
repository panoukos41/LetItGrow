using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Node.Models;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Node.Requests
{
    /// <summary>
    /// A request to add a group to a node.
    /// </summary>
    public record GroupAdd : BaseUpdate
    {
        /// <summary>
        /// The id of the group that the node will be added to.
        /// </summary>
        [JsonPropertyName("groupId")]
        public string GroupId { get; set; } = string.Empty;

        public GroupAdd()
        {
        }

        public GroupAdd(NodeModel node)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
        }

        public GroupAdd(NodeModel node, GroupModel group)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
            GroupId = group.Id;
        }
    }
}