using LetItGrow.Identity.Application.Models;
using MediatR;

namespace LetItGrow.Identity.Application.Commands
{
    public record CreateApplication : IRequest<ApplicationModel>
    {
        public string Name { get; set; } = string.Empty;
    }
}