using LetItGrow.Identity.Application.Commands;
using LetItGrow.Identity.Application.Models;
using MediatR;
using OpenIddict.Core;
using OpenIddict.CouchDB.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Application.CommandHandlers
{
    public class CreateApplicationHandler : IRequestHandler<CreateApplication, ApplicationModel>
    {
        private readonly OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager;

        public CreateApplicationHandler(OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager)
        {
            this.manager = manager;
        }

        public Task<ApplicationModel> Handle(CreateApplication request, CancellationToken cancellationToken)
        {
            // todo: CreateApplication

            throw new NotImplementedException();
        }
    }
}