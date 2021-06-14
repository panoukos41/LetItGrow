using LetItGrow.Microservice.Group.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Group.Notifications
{
    public record GroupCreated : INotification
    {
        [JsonPropertyName("group")]
        public GroupModel Group { get; }

        public GroupCreated(GroupModel group)
        {
            Group = group;
        }
    }
}