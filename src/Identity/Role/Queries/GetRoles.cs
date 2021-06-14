using LetItGrow.Identity.Role.Models;
using MediatR;

namespace LetItGrow.Identity.Role.Queries
{
    public record GetRoles : IRequest<RoleModel[]>
    {
    }
}