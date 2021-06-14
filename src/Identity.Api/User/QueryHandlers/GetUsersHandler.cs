using AspNetCore.Identity.CouchDB.Models;
using CouchDB.Driver;
using LetItGrow.Identity.Role.Extensions;
using LetItGrow.Identity.User.Extensions;
using LetItGrow.Identity.User.Models;
using LetItGrow.Identity.User.Queries;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.User.QueryHandlers
{
    using View = AspNetCore.Identity.CouchDB.Internal.Views<CouchDbUser, CouchDbRole>;

    public class GetUsersHandler : IRequestHandler<GetUsers, UserModel[]>
    {
        private readonly ICouchDatabase<CouchDbUser> db;

        public GetUsersHandler(ICouchClient couchClient, IConfiguration configuration)
        {
            db = couchClient.GetDatabase<CouchDbUser>(configuration.GetCouchDatabase());
        }

        public async Task<UserModel[]> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            return (await db.GetViewAsync<string, string>(View.User.Design, View.User.Value, new() { Reduce = false, IncludeDocs = true }, cancellationToken))
               .Select(x => x.Document.ToModel())
               .ToArray();
        }
    }
}