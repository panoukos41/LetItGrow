using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace LetItGrow.UI.Common
{
    public static class Events
    {
        private static readonly Subject<HubConnectionState> _connection = new();

        public static IObservable<HubConnectionState> WhenConnection() => _connection.AsObservable();

        public static void Publish(HubConnectionState state) => _connection.OnNext(state);
    }
}