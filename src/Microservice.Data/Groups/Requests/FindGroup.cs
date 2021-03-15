using LetItGrow.Microservice.Data.Groups.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Groups.Requests
{
    /// <summary>
    /// A request to get a node group.
    /// </summary>
    public record FindGroup : IRequest<GroupModel>
    {
        /// <summary>
        /// The id of the group
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; init; } = string.Empty;

        public FindGroup()
        {
        }

        public FindGroup(string id)
        {
            Id = id;
        }
    }
}