using LetItGrow.Identity.Common.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.User.Commands
{
    public record RoleAdd : IRequest<UpdateModel>
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("role_name")]
        public string RoleName { get; set; } = string.Empty;
    }
}