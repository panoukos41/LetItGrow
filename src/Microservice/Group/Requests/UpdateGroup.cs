using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Group.Models;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Group.Requests
{
    /// <summary>
    /// A request to update a node group.
    /// </summary>
    public record UpdateGroup : BaseUpdate
    {
        /// <summary>
        /// A new name for the group. if it's null it won't update.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// A new description for the group. If it's null it won't update.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// A new type for the group. If it's null it won't update.
        /// </summary>
        [JsonPropertyName("type")]
        public GroupType? Type { get; set; }

        public UpdateGroup()
        {
        }

        public UpdateGroup(string id, string concurrencyStamp)
        {
            Id = id;
            ConcurrencyStamp = concurrencyStamp;
        }

        public UpdateGroup(GroupModel group)
        {
            Id = group.Id;
            ConcurrencyStamp = group.ConcurrencyStamp;
        }
    }
}