using LetItGrow.Microservice.RestApi.Hubs.Internal;
using MediatR;
using System;
using System.Threading.Tasks;

namespace LetItGrow.Microservice.RestApi.Hubs
{
    public partial class ApiHub : HubBase
    {
        public ApiHub(IMediator mediator) : base(mediator)
        {
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}