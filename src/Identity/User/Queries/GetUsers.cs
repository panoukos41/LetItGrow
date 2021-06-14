using LetItGrow.Identity.User.Models;
using MediatR;

namespace LetItGrow.Identity.User.Queries
{
    public record GetUsers : IRequest<UserModel[]>
    {
    }
}