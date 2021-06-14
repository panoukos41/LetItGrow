using LetItGrow.Identity.Application.Models;
using MediatR;

namespace LetItGrow.Identity.Application.Queries
{
    public record GetApplications : IRequest<ApplicationModel[]>
    {
    }
}