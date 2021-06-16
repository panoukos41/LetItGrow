using LetItGrow.Microservice.Common;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.UI.Common.Services
{
    public class HubService
    {
        private HubConnection Hub { get; }

        public HubConnectionState State => Hub.State;

        public IObservable<Exception> WhenDisconnected { get; }

        public IObservable<Exception> WhenReconnecting { get; }

        public IObservable<string> WhenReconnected { get; }

        public HubService(HubConnection hub)
        {
            Hub = hub;
            Hub.Closed += async (arg) =>
            {
                await Task.Delay(5000);
                await Hub.StartAsync();
            };

            WhenDisconnected = Observable.FromEvent<Func<Exception, Task>, Exception>(
                converter => args => { converter(args); return Task.CompletedTask; },
                handler => Hub.Closed += handler,
                handler => Hub.Closed -= handler);

            WhenReconnecting = Observable.FromEvent<Func<Exception, Task>, Exception>(
                converter => args => { converter(args); return Task.CompletedTask; },
                handler => Hub.Reconnecting += handler,
                handler => Hub.Reconnecting -= handler);

            WhenReconnected = Observable.FromEvent<Func<string, Task>, string>(
                converter => args => { converter(args); return Task.CompletedTask; },
                handler => Hub.Reconnected += handler,
                handler => Hub.Reconnected -= handler);

            WhenReconnecting.Subscribe(ex => Events.Publish(HubConnectionState.Reconnecting));
            WhenReconnected.Subscribe(ex => Events.Publish(HubConnectionState.Connected));
            WhenDisconnected.Subscribe(ex => Events.Publish(HubConnectionState.Disconnected));
        }

        public async Task ConnectAsync(CancellationToken token = default)
        {
            if (Hub.State != HubConnectionState.Disconnected) return;

            Events.Publish(HubConnectionState.Connecting);

            await Hub.StartAsync(token);

            Events.Publish(HubConnectionState.Connected);
        }

        public void On<T1>(string methodName, Action<T1> handler) =>
            Hub.On(methodName, handler);

        public async Task<Result<TResponse>> SendAsync<TResponse>(string methodName, IRequest<TResponse> request, CancellationToken token = default)
        {
            while (Hub.State != HubConnectionState.Connected) await Task.Delay(500, token);

            try
            {
                await Task.Delay(1000, token);

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