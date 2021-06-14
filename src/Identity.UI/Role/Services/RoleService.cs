using Flurl.Http;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Role.Commands;
using LetItGrow.Identity.Role.Models;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Role.Services
{
    public class RoleService
    {
        private readonly IFlurlClient client;
        private IFlurlRequest Request => client.Request("api/v1/role");

        public RoleService(IFlurlClient client)
        {
            this.client = client;
        }

        public async Task<Result<RoleModel>> CreateAsync(CreateRole request)
        {
            return await Request
                .PostJsonAsync(request)
                .ReceiveJson<RoleModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<Unit>> DeleteAsync(DeleteRole request)
        {
            await Request
                .SendJsonAsync(HttpMethod.Delete, request)
                .SendRequestAsync()
                .ConfigureAwait(false);

            return Unit.Default;
        }

        public async Task<Result<RoleModel>> GetAsync(string id)
        {
            return await Request
                .AppendPathSegment(id)
                .GetJsonAsync<RoleModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<RoleModel[]>> GetAllAsync()
        {
            return await Request
                .GetJsonAsync<RoleModel[]>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }
    }
}