using LetItGrow.Microservice.Abstraction.Stores;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Node.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Microservice.Worker
{
    public class Worker : BackgroundService, IIrrigator
    {
        private readonly ILogger<Worker> logger;
        private readonly INodeStore nodeStore;
        private readonly IGroupStore groupStore;
        private readonly IIrrigationStore irrigationStore;
        private readonly IMeasurementStore measurementStore;
        private readonly IMemoryCache cache;
        private readonly IObservable<long> interval;

        public Worker(ILogger<Worker> logger,
            INodeStore nodeStore,
            IGroupStore groupStore,
            IIrrigationStore irrigationStore,
            IMeasurementStore measurementStore,
            IMemoryCache cache,
            IConfiguration configuration)
        {
            this.logger = logger;
            this.nodeStore = nodeStore;
            this.groupStore = groupStore;
            this.irrigationStore = irrigationStore;
            this.measurementStore = measurementStore;
            this.cache = cache;
            var period = configuration.GetValue("period", 15);
            interval = Observable.Interval(TimeSpan.FromMinutes(period));
            logger.LogInformation("Period set to '{period}'", period);
        }

        public async Task Irrigate(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested) return;

            logger.LogInformation("Irrigation calculation started");

            var groups = (await groupStore.Search(new(), stoppingToken))
                .Where(x => x.Type is not GroupType.None);

            var start = DateTimeOffset.UtcNow.AddDays(1);
            var end = start.AddHours(-1).AddDays(-1);

            var actions = await groups
                .Select(group => Calculate(group, start, end, stoppingToken));

            await actions
                .Select(action => Update(action, stoppingToken));

            logger.LogInformation("Irrigation calculation ended");
        }

        private async Task<(string[] Ids, bool Status)> Calculate(GroupModel group, DateTimeOffset start, DateTimeOffset end, CancellationToken token)
        {
            var nodes = await nodeStore.Search(new() { GroupId = group.Id }, token);

            var measurements = await measurementStore.SearchMany(
                cancellationToken: token,
                request: nodes
                    .Where(n => n.Type == NodeType.Measurement)
                    .Select(n => new SearchMeasurements
                    {
                        NodeId = n.Id,
                        StartDate = start,
                        EndDate = end
                    }).ToArray());

            if (measurements.Length == 0) return (Array.Empty<string>(), false);

            var moisture = measurements.Average(m => m.SoilMoisture);

            var status = group.Type switch
            {
                GroupType.Potatoes => moisture <= 50,
                _ => false
            };

            var ids = nodes
                .Where(n => n.Type == NodeType.Irrigation)
                .Select(x => x.Id)
                .ToArray();

            return (ids, status);
        }

        private async Task Update((string[] Ids, bool status) action, CancellationToken token)
        {
            var (ids, status) = action;

            if (ids.Length == 0) return;

            var tocache = ids.Where(id => cache.TryGetValue(id, out _) is false).ToHashSet();

            var cachedTask = Task.Run(async () => await ids
                .Where(id => tocache.Contains(id) is false)
                .Where(id => cache.Get<bool>(id) != status)
                .Select(id => irrigationStore.Create(
                cancellationToken: token,
                request: new()
                {
                    IssuedAt = DateTimeOffset.UtcNow,
                    NodeId = id,
                    Type = status ? IrrigationType.Start : IrrigationType.Stop
                })));

            var notCachedTask = Task.Run(async () => await tocache
                .Select(id => irrigationStore.Create(
                cancellationToken: token,
                request: new()
                {
                    IssuedAt = DateTimeOffset.UtcNow,
                    NodeId = id,
                    Type = status ? IrrigationType.Start : IrrigationType.Stop
                })));

            foreach (var id in ids)
            {
                Cache(id, status);
            }

            await Task.WhenAll(cachedTask, notCachedTask);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var nodes = await nodeStore.Search(new(), stoppingToken);

            var irrigations = await irrigationStore.SearchMany(
                cancellationToken: stoppingToken,
                request: nodes
                    .Where(n => n.Type == NodeType.Irrigation)
                    .Select(n => new SearchIrrigations
                    {
                        NodeId = n.Id,
                        StartDate = DateTimeOffset.UtcNow.AddDays(1),
                        EndDate = DateTimeOffset.UtcNow.AddDays(-8)
                    }).ToArray());

            var statuses = irrigations
                .GroupBy(i => i.NodeId)
                .Select(i => i.First())
                .Select(i => (i.NodeId, i.Type == IrrigationType.Start));

            foreach (var (id, status) in statuses)
            {
                Cache(id, status);
            }

            Observable.FromAsync(Irrigate).Subscribe(stoppingToken);
            interval.Subscribe(
                token: stoppingToken,
                onNext: t => Observable.FromAsync(Irrigate).Subscribe(stoppingToken));

            await interval.ToTask(stoppingToken);
        }

        private void Cache(string id, bool status) =>
            cache.Set(id, status, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(1)
            });
    }
}