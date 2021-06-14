using Flurl.Http;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Common.Models;
using LetItGrow.Identity.User.Commands;
using LetItGrow.Identity.User.Models;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;

namespace LetItGrow.Identity.User.Services
{
    public class UserService
    {
        private readonly IFlurlClient client;
        private IFlurlRequest Request => client.Request("api/v1/user");

        public UserService(IFlurlClient client)
        {
            this.client = client;
        }

        public async Task<Result<UserModel>> CreateAsync(CreateUser request)
        {
            return await Request
                .PostJsonAsync(request)
                .ReceiveJson<UserModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<Unit>> DeleteAsync(DeleteUser request)
        {
            await Request
                .SendJsonAsync(HttpMethod.Delete, request)
                .SendRequestAsync()
                .ConfigureAwait(false);

            return Unit.Default;
        }

        public async Task<Result<UserModel>> GetAsync(string id)
        {
            return await Request
                .AppendPathSegment(id)
                .GetJsonAsync<UserModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<UserModel[]>> GetAllAsync()
        {
            return await Request
                .GetJsonAsync<UserModel[]>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<UpdateModel>> AddRole(RoleAdd request)
        {
            return await Request
                .AppendPathSegment("role")
                .PutJsonAsync(request)
                .ReceiveJson<UpdateModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<UpdateModel>> RemoveRole(RoleRemove request)
        {
            return await Request
                .AppendPathSegment("role")
                .SendJsonAsync(HttpMethod.Delete, request)
                .ReceiveJson<UpdateModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }
    }
}