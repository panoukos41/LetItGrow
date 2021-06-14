using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.NodeIot.Services;
using LetItGrow.NodeIot.Workers.Internal;
using MQTTnet;
using MQTTnet.Extensions.External.RxMQTT.Client;
using Serilog;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.NodeIot.Workers
{
    public class IrrigationWorker : Worker
    {
        private readonly IRxMqttClient _client;
        private readonly IIrrigationService _irrigations;

        protected override string Id { get; } = nameof(IrrigationWorker);

        public IrrigationWorker(IRxMqttClient client, IIrrigationService irrigations)
        {
            _client = client;
            _irrigations = irrigations;
        }

        private IrrigationSettings Settings = new();

        private Subject<bool> Status { get; } = new();

        public override Task Initialize(CancellationToken stop)
        {
            _client
            .Connect($"{Config.TopicPrefix}/settings/{Config.ClientId}")
            .ObserveOn(ThreadPoolScheduler.Instance)
            .Select(args => args.ApplicationMessage.GetPayload<IrrigationSettings>())
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

            _client
            .Connect($"{Config.TopicPrefix}/irrigations/{Config.ClientId}")
            .ObserveOn(ThreadPoolScheduler.Instance)
            .Select(args => args.ApplicationMessage.GetPayload<CreateIrrigation>())
            .SubscribeOn(ThreadPoolScheduler.Instance)
            .Subscribe(
            token: stop,
            onNext: irrigation =>
            {
                //if (irrigation.IssuedAt - SystemClock.Instance.GetCurrentInstant() < Duration.FromHours(1)) return;

                // todo: Use settings to check the last known irrigation,
                // in case you can't connect then stop irrigating.
                if (irrigation is { Type: IrrigationType.Start })
                {
                    _irrigations.Set(true);
                    return;
                }

                _irrigations.Set(false);
            });

            return Task.CompletedTask;
        }

        protected override Task WorkAsync(CancellationToken stop)
        {
            return Task.Delay(Timeout.Infinite, stop).Ignore();

            //DateTimeOffset start = DateTimeOffset.Now;
            //DateTimeOffset finish = start.AddSeconds(Settings.PollInterval);

            //await Task.Delay(finish.Subtract(start), stop).Ignore().ConfigureAwait(false);

            //if (stop.IsCancellationRequested) return;

            //Measure();
        }
    }
}