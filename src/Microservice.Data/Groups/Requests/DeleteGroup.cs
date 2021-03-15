using LetItGrow.Microservice.Data.Groups.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Groups.Requests
{
    /// <summary>
    /// A request to delete a node group.
    /// </summary>
    public record DeleteGroup : IRequest<Unit>
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

        public DeleteGroup()
        {
        }

        public DeleteGroup(string id, string concurrencyStamp)
        {
            Id = id;
            ConcurrencyStamp = concurrencyStamp;
        }

        public DeleteGroup(GroupModel group)
        {
            Id = group.Id;
            ConcurrencyStamp = group.ConcurrencyStamp;
        }
    }
}