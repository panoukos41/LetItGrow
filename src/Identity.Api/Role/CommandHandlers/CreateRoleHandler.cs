using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Role.Commands;
using LetItGrow.Identity.Role.Extensions;
using LetItGrow.Identity.Role.Models;
using LetItGrow.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Role.CommandHandlers
{
    public class CreateRoleHandler : IRequestHandler<CreateRole, RoleModel>
    {
        private readonly RoleManager<CouchDbRole> manager;
        private readonly IPrimaryKeyService primaryKey;

        public CreateRoleHandler(RoleManager<CouchDbRole> manager, IPrimaryKeyService primaryKey)
        {
            this.manager = manager;
            this.primaryKey = primaryKey;
        }

        public async Task<RoleModel> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var role = new CouchDbRole(request.Name)
            {
                Id = primaryKey.Create()
            };

            var result = await manager.CreateAsync(role);

            if (result.Succeeded)
            {
                return role.ToModel();
            }

            throw new ErrorException(Errors.Conflict with
            {
                Detail = "Name already exists",
                Metadata = result.Errors.ToDictionary(
                    key => key.Code,
                    val => val.Description)
            });
        }
    }
}