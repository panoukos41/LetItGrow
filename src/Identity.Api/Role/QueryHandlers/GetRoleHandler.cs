using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Role.Extensions;
using LetItGrow.Identity.Role.Models;
using LetItGrow.Identity.Role.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Role.QueryHandlers
{
    public class GetRoleHandler : IRequestHandler<GetRole, RoleModel>
    {
        private readonly RoleManager<CouchDbRole> manager;

        public GetRoleHandler(RoleManager<CouchDbRole> manager)
        {
            this.manager = manager;
        }

        public async Task<RoleModel> Handle(GetRole request, CancellationToken cancellationToken)
        {
            return await manager.FindByIdAsync(request.Id)
                is { } role
                ? role.ToModel()
                : throw new ErrorException(Errors.NotFound);
        }
    }
}