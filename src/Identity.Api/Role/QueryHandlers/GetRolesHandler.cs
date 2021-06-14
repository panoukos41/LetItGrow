using AspNetCore.Identity.CouchDB.Models;
using CouchDB.Driver;
using LetItGrow.Identity.Role.Extensions;
using LetItGrow.Identity.Role.Models;
using LetItGrow.Identity.Role.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Role.QueryHandlers
{
    using View = AspNetCore.Identity.CouchDB.Internal.Views<CouchDbUser, CouchDbRole>;

    public class GetRolesHandler : IRequestHandler<GetRoles, RoleModel[]>
    {
        private readonly ICouchDatabase<CouchDbRole> db;

        public GetRolesHandler(ICouchClient couchClient, IConfiguration configuration)
        {
            db = couchClient.GetDatabase<CouchDbRole>(configuration.GetCouchDatabase());
        }

        public async Task<RoleModel[]> Handle(GetRoles request, CancellationToken cancellationToken)
        {
            return (await db.GetViewAsync<string, string>(View.Role.Design, View.Role.Value, new() { Reduce = false, IncludeDocs = true }, cancellationToken))
                .Select(x => x.Document.ToModel())
                .ToArray();
        }
    }
}