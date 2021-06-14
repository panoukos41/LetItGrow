using LetItGrow.Identity.Application.Extensions;
using LetItGrow.Identity.Application.Models;
using LetItGrow.Identity.Application.Queries;
using MediatR;
using OpenIddict.Core;
using OpenIddict.CouchDB.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Application.QueryHandlers
{
    //using View = OpenIddict.CouchDB.Internal.Views.Application<CouchDbOpenIddictApplication>;

    public class GetApplicationsHandler : IRequestHandler<GetApplications, ApplicationModel[]>
    {
        private readonly OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager;

        public GetApplicationsHandler(OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager)
        {
            this.manager = manager;
        }

        public async Task<ApplicationModel[]> Handle(GetApplications request, CancellationToken cancellationToken)
        {
            // todo: maybe use the couch view.

            var buffer = new List<ApplicationModel>();

            await foreach (var other in manager.ListAsync(cancellationToken: cancellationToken))
            {
                buffer.Add(other.ToModel());
            }

            return buffer.ToArray();
        }
    }
}