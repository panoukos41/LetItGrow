using Flurl.Http;
using LetItGrow.Identity.Application.Commands;
using LetItGrow.Identity.Application.Models;
using LetItGrow.Identity.Common;
using LetItGrow.Identity.Common.Models;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Application.Services
{
    public class ApplicationService
    {
        private readonly IFlurlClient client;
        private IFlurlRequest Request => client.Request("api/v1/app");

        public ApplicationService(IFlurlClient client)
        {
            this.client = client;
        }

        public async Task<Result<ApplicationModel>> CreateAsync(CreateApplication request)
        {
            return await Request
                .PostJsonAsync(request)
                .ReceiveJson<ApplicationModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<UpdateModel>> UpdateAsync(UpdateApplication request)
        {
            return await Request
                .PutJsonAsync(request)
                .ReceiveJson<UpdateModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<Unit>> DeleteAsync(DeleteApplication request)
        {
            await Request
                .SendJsonAsync(HttpMethod.Delete, request)
                .SendRequestAsync()
                .ConfigureAwait(false);

            return Unit.Default;
        }

        public async Task<Result<ApplicationModel>> GetAsync(string id)
        {
            return await Request
                .AppendPathSegment(id)
                .GetJsonAsync<ApplicationModel>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }

        public async Task<Result<ApplicationModel[]>> GetAllAsync()
        {
            return await Request
                .GetJsonAsync<ApplicationModel[]>()
                .SendRequestAsync()
                .ConfigureAwait(false);
        }
    }
}