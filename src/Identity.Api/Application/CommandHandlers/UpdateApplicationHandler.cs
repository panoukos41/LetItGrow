using LetItGrow.Identity.Application.Commands;
using LetItGrow.Identity.Common.Models;
using MediatR;
using OpenIddict.Core;
using OpenIddict.CouchDB.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Application.CommandHandlers
{
    public class UpdateApplicationHandler : IRequestHandler<UpdateApplication, UpdateModel>
    {
        private readonly OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager;

        public UpdateApplicationHandler(OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager)
        {
            this.manager = manager;
        }

        public Task<UpdateModel> Handle(UpdateApplication request, CancellationToken cancellationToken)
        {
            // todo: Application update.
            throw new NotImplementedException();
        }
    }
}