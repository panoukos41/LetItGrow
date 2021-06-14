using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Role.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Role.CommandHandlers
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRole, Unit>
    {
        private readonly RoleManager<CouchDbRole> manager;

        public DeleteRoleHandler(RoleManager<CouchDbRole> manager)
        {
            this.manager = manager;
        }

        public async Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            try
            {
                await manager.DeleteAsync(new() { Id = request.Id, Rev = request.Rev });
            }
            catch (Exception ex)
            {
                throw new ErrorException(Errors.NotFound, ex);
            }
            return Unit.Value;
        }
    }
}