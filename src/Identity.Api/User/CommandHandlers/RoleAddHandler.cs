using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Common.Models;
using LetItGrow.Identity.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.User.CommandHandlers
{
    public class RoleAddHandler : IRequestHandler<RoleAdd, UpdateModel>
    {
        private readonly UserManager<CouchDbUser> manager;

        public RoleAddHandler(UserManager<CouchDbUser> manager)
        {
            this.manager = manager;
        }

        public async Task<UpdateModel> Handle(RoleAdd request, CancellationToken cancellationToken)
        {
            var user = await manager.FindByIdAsync(request.UserId)
                ?? throw new ErrorException(Errors.NotFound);

            var result = await manager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return new(user.Id, user.Rev);
            }

            throw new ErrorException(Errors.NotFound with
            {
                Detail = $"Could not find role '{request.RoleName}'"
            });
        }
    }
}