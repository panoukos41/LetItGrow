using LetItGrow.Identity.Application.Extensions;
using LetItGrow.Identity.Application.Models;
using LetItGrow.Identity.Application.Queries;
using LetItGrow.Identity.Common;
using MediatR;
using OpenIddict.Core;
using OpenIddict.CouchDB.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Application.QueryHandlers
{
    public class GetApplicationHandler : IRequestHandler<GetApplication, ApplicationModel>
    {
        private readonly OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager;

        public GetApplicationHandler(OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager)
        {
            this.manager = manager;
        }

        public async Task<ApplicationModel> Handle(GetApplication request, CancellationToken cancellationToken)
        {
            return await manager.FindByIdAsync(request.Id, cancellationToken)
                is { } app
                ? app.ToModel()
                : throw new ErrorException(Errors.NotFound);
        }
    }
}