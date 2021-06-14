using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.User.Extensions;
using LetItGrow.Identity.User.Models;
using LetItGrow.Identity.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.User.QueryHandlers
{
    public class GetUserHandler : IRequestHandler<GetUser, UserModel>
    {
        private readonly UserManager<CouchDbUser> manager;

        public GetUserHandler(UserManager<CouchDbUser> manager)
        {
            this.manager = manager;
        }

        public async Task<UserModel> Handle(GetUser request, CancellationToken cancellationToken)
        {
            return await manager.FindByIdAsync(request.Id)
                is { } user
                ? user.ToModel()
                : throw new ErrorException(Errors.NotFound);
        }
    }
}