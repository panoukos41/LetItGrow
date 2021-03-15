using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Groups.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to add a node to a group.
    /// </summary>
    public record AddNodeToGroup : IRequest<ModelUpdate>
    {
        /// <summary>
        /// The id of the node.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the node.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

        /// <summary>
        /// The id of the group that the node will be added to.
        /// </summary>
        [JsonPropertyName("groupId")]
        public string GroupId { get; init; } = string.Empty;

        public AddNodeToGroup()
        {
        }

        public AddNodeToGroup(NodeModel node, GroupModel group)
        {
            Id = node.Id;
            ConcurrencyStamp = node.ConcurrencyStamp;
            GroupId = group.Id;
        }
    }
}