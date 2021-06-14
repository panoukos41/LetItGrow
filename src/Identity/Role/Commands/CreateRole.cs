using LetItGrow.Identity.Role.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Role.Commands
{
    public record CreateRole : IRequest<RoleModel>
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}