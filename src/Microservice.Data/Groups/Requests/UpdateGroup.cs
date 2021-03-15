using LetItGrow.Microservice.Data.Common;
using LetItGrow.Microservice.Data.Groups.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Groups.Requests
{
    /// <summary>
    /// A request to update a node group.
    /// </summary>
    public record UpdateGroup : IRequest<ModelUpdate>
    {
        /// <summary>
        /// The id of the group.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The concurrency stamp of the group.
        /// </summary>
        [JsonPropertyName("concurrencyStamp")]
        public string ConcurrencyStamp { get; init; } = string.Empty;

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