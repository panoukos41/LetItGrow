using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.NodeIot.Services;
using LetItGrow.NodeIot.Workers.Internal;
using MQTTnet;
using MQTTnet.Extensions.External.RxMQTT.Client;
using Serilog;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.NodeIot.Workers
{
    public class MeasurementWorker : Worker
    {
        private readonly IRxMqttClient _client;
        private readonly IMeasurementService _measurements;
        protected override string Id { get; } = nameof(MeasurementWorker);

        public MeasurementWorker(IRxMqttClient client, IMeasurementService measurements)
        {
            _client = client;
            _measurements = measurements;
        }

        private MeasurementSettings Settings = new();

        public override Task Initialize(CancellationToken stop)
        {
            _client
            .Connect($"{Config.TopicPrefix}/settings/{Config.ClientId}")
            .ObserveOn(ThreadPoolScheduler.Instance)
            .Select(args => args.ApplicationMessage.GetPayload<MeasurementSettings>())
            .SubscribeOn(ThreadPoolScheduler.Instance)
            .Subscribe(
            token: stop,
            onNext: settings =>
            {
                if (settings is null ||
                    settings == Settings) return;

                if (settings is
                    {
                        PollInterval: > 3600 or < 60
                    })
                {
                    Log.Warning("Invalid settings `{settings}` received", JsonSerializer.Serialize(settings));
                    return;
                }

                Settings = settings;
            });

            return Task.CompletedTask;
        }

        protected override async Task WorkAsync(CancellationToken stop)
        {
            DateTimeOffset start = DateTimeOffset.Now;
            DateTimeOffset finish = start.AddSeconds(Settings.PollInterval);

            await Task.Delay(finish.Subtract(start), stop).Ignore().ConfigureAwait(false);

            if (stop.IsCancellationRequested) return;

            Measure();
        }

        protected void Measure()
        {
            var (tempC, humidity, soilMoisture) = _measurements.Get();
            var topic = $"{Config.TopicPrefix}/measurement/{Config.ClientId}";
            _client.Publish(topic, new CreateMeasurement
            {
                NodeId = Config.ClientId,
                MeasuredAt = DateTimeOffset.UtcNow,
                AirTemperatureC = tempC,
                AirHumidity = humidity,
                SoilMoisture = soilMoisture
            });
        }
    }
}