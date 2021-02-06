using LetItGrow.UI.Services.Internal;
using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public class DashboardService : HubServiceBase, IDashboardService
    {
        private readonly Subject<NodeModel> nodeAdded = new();
        private readonly Subject<NodeGroupModel> nodeGroupAdded = new();

        public HubConnectionState State => Hub.State;

        public IObservable<NodeModel> WhenNodeAdded { get; }

        public IObservable<NodeGroupModel> WhenNodeGroupAdded { get; }

        public IObservable<Exception> WhenDisconnected { get; }

        public IObservable<Exception> WhenReconnecting { get; }

        public IObservable<string> WhenReconnected { get; }

        public DashboardService(HubConnection hub) : base(hub)
        {
            WhenNodeAdded = nodeAdded.AsObservable();
            WhenNodeGroupAdded = nodeGroupAdded.AsObservable();

            hub.On<NodeModel>("node:added", nodeAdded.OnNext);
            hub.On<NodeGroupModel>("nodegroup:added", nodeGroupAdded.OnNext);

            WhenDisconnected = Observable.FromEvent<Func<Exception, Task>, Exception>(
                converter => args => { converter(args); return Task.CompletedTask; },
                handler => hub.Closed += handler,
                handler => hub.Closed -= handler);

            WhenReconnecting = Observable.FromEvent<Func<Exception, Task>, Exception>(
                converter => args => { converter(args); return Task.CompletedTask; },
                handler => hub.Reconnecting += handler,
                handler => hub.Reconnecting -= handler);

            WhenReconnected = Observable.FromEvent<Func<string, Task>, string>(
                converter => args => { converter(args); return Task.CompletedTask; },
                handler => hub.Reconnected += handler,
                handler => hub.Reconnected -= handler);
        }

        public async Task<bool> ConnectAsync(CancellationToken token)
        {
            // todo: imporve hub connect as in the docs
            // https://docs.microsoft.com/en-us/aspnet/core/signalr/dotnet-client?view=aspnetcore-5.0&tabs=visual-studio#handle-lost-connection

            await Hub.StartAsync(token);

            return true;
        }
    }
}