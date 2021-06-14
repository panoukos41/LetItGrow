using LetItGrow.Identity.User.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.User.Commands
{
    public record CreateUser : IRequest<UserModel>
    {
        [JsonPropertyName("username")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}