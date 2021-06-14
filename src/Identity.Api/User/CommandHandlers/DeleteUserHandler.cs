using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.User.CommandHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser, Unit>
    {
        private readonly UserManager<CouchDbUser> manager;

        public DeleteUserHandler(UserManager<CouchDbUser> manager)
        {
            this.manager = manager;
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
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