using LetItGrow.Microservice.Data;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services.Internal
{
    public abstract class HubServiceBase
    {
        protected HubConnection Hub { get; }

        public HubServiceBase(HubConnection hub)
        {
            Hub = hub;
        }

        protected async Task<Result<TResponse>> Handle<TResponse>(string methodName, IRequest<TResponse> request, CancellationToken token = default)
        {
            try
            {
                return await Hub.InvokeAsync<TResponse>(methodName, request, token);
            }
            catch (Exception ex)
            {
                return ExceptionHandler(ex);
            }
        }

        protected static Error ExceptionHandler(Exception exception) => exception switch
        {
            HubException { Message: var msg } => msg.Contains('~')
                    ? JsonSerializer.Deserialize<Error>(msg.Split('~')[1])!
                    : new Error(title: "Dashboard Hub error", detail: msg, status: exception.HResult),

            _ => new Error(title: "Unknown Error", detail: exception.Message, status: exception.HResult)
        };
    }
}