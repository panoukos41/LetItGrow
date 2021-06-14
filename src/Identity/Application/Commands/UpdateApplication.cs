using LetItGrow.Identity.Common.Models;
using MediatR;

namespace LetItGrow.Identity.Application.Commands
{
    public record UpdateApplication : IRequest<UpdateModel>
    {
    }
}