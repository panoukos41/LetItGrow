using LetItGrow.Microservice.Group.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Group.Notifications
{
    public record GroupDeleted : INotification
    {
        [JsonPropertyName("groupId")]
        public string GroupId { get; }

        [JsonPropertyName("rev")]
        public string Rev { get; set; }

        public GroupDeleted(string id, string rev)
        {
            GroupId = id;
            Rev = rev;
        }

        public GroupDeleted(GroupModel group)
        {
            GroupId = group.Id;
            Rev = group.ConcurrencyStamp;
        }
    }
}