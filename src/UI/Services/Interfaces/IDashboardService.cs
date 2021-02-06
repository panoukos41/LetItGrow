using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.UI.Services
{
    public interface IDashboardService
    {
        // todo: Implement IDashboardService

        HubConnectionState State { get; }

        IObservable<NodeModel> WhenNodeAdded { get; }

        IObservable<NodeGroupModel> WhenNodeGroupAdded { get; }

        IObservable<Exception> WhenDisconnected { get; }

        IObservable<Exception> WhenReconnecting { get; }

        IObservable<string> WhenReconnected { get; }

        Task<bool> ConnectAsync(CancellationToken token);
    }
}