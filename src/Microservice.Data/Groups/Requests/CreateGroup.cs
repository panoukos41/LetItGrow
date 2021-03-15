using LetItGrow.Microservice.Data.Groups.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Groups.Requests
{
    /// <summary>
    /// A request to create a new node group.
    /// </summary>
    public record CreateGroup : IRequest<GroupModel>
    {
        /// <summary>
        /// A name for the group.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A small description for the group. (optional)
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        /// The type of the group.
        /// </summary>
        [JsonPropertyName("type")]
        public GroupType Type { get; set; }

        public CreateGroup()
        {
        }

        public CreateGroup(string name, string? description = null, GroupType type = GroupType.None)
        {
            Name = name;
            Description = description;
            Type = type;
        }
    }
}