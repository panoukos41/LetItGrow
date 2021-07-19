using LetItGrow.NodeIot.Common;
using Microsoft.Extensions.Hosting;
using MQTTnet.Extensions.External.RxMQTT.Client;
using MQTTnet.Extensions.ManagedClient;
using Serilog;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.NodeIot.Workers
{
    public class MqttWorker : IHostedService
    {
        private readonly IRxMqttClient _client;
        private readonly IManagedMqttClientOptions _options;

        public MqttWorker(IRxMqttClient client, IManagedMqttClientOptions options)
        {
            _client = client;
            _options = options;
        }

        protected string Id { get; } = "MqttClient";

        protected readonly Pin connectionPin = null!;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _client.Connected
                .ObserveOn(ThreadPoolScheduler.Instance)
                .Distinct()
                .Throttle(TimeSpan.FromMilliseconds(500))
                .Subscribe(state =>
                {
                    Log.Information("'{Id}' connection state '{state}'", Id, state);
                });
            await _client.StartAsync(_options);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync();
        }
    }
}