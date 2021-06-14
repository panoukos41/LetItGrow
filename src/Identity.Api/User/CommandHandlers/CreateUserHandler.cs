using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.User.Commands;
using LetItGrow.Identity.User.Extensions;
using LetItGrow.Identity.User.Models;
using LetItGrow.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.User.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, UserModel>
    {
        private readonly UserManager<CouchDbUser> manager;
        private readonly IPrimaryKeyService primaryKey;

        public CreateUserHandler(UserManager<CouchDbUser> manager, IPrimaryKeyService primaryKey)
        {
            this.manager = manager;
            this.primaryKey = primaryKey;
        }

        public async Task<UserModel> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new CouchDbUser(request.UserName)
            {
                Id = primaryKey.Create()
            };
            user.Roles.Add(new CouchDbRole("User"));

            var result = await manager.CreateAsync(user, request.Password);

            if (result.Succeeded) return user.ToModel();

            throw new ErrorException(Errors.Conflict with
            {
                Detail = "Username already exists",
                Metadata = result.Errors.ToDictionary(
                   key => key.Code,
                   val => val.Description)
            });
        }
    }
}