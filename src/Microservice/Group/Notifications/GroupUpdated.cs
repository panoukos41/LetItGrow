using LetItGrow.Microservice.Group.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Group.Notifications
{
    public record GroupUpdated : INotification
    {
        [JsonPropertyName("group")]
        public GroupModel Group { get; }

        public GroupUpdated(GroupModel group)
        {
            Group = group;
        }
    }
}