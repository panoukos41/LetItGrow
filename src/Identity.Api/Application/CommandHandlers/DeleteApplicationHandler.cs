using LetItGrow.Identity.Application.Commands;
using MediatR;
using OpenIddict.Core;
using OpenIddict.CouchDB.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Application.CommandHandlers
{
    public class DeleteApplicationHandler : IRequestHandler<DeleteApplication, Unit>
    {
        private readonly OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager;

        public DeleteApplicationHandler(OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager)
        {
            this.manager = manager;
        }

        public async Task<Unit> Handle(DeleteApplication request, CancellationToken cancellationToken)
        {
            await manager.DeleteAsync(new()
            {
                Id = request.Id,
                Rev = request.Rev
            }, cancellationToken);

            return Unit.Value;
        }
    }
}