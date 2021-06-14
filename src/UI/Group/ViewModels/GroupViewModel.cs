using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.UI.Common;
using LetItGrow.UI.Group.Services;
using LetItGrow.UI.Node.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LetItGrow.UI.Group.ViewModels
{
    public class GroupViewModel : RxViewModel
    {
        [Reactive]
        public GroupModel Group { get; set; }

        [Reactive]
        public NodeModel[]? Nodes { get; set; }

        [Reactive]
        public (IrrigationModel[] irrigations, MeasurementModel[] measurements)? NodeData { get; set; }

        [Reactive]
        public SearchOptions SearchOptions { get; set; }

        public ReactiveCommand<Unit, Unit> LoadNodes { get; }

        public ReactiveCommand<Unit, Unit> LoadNodeData { get; }

        public GroupViewModel(string groupId, IGroupService groupService, INodeService nodeService)
        {
            Loading = true;
            Group = null!;
            SearchOptions = new();

            LoadNodes = ReactiveCommand.CreateFromTask(async () =>
                await nodeService.Search(new() { GroupId = Group.Id }).SwitchAsync(
                    s => Nodes = s,
                    e => Console.WriteLine(e)));

            LoadNodeData = ReactiveCommand.CreateFromTask(
                canExecute: this.WhenAnyValue(x => x.Nodes, n => n is { Length: > 0 }),
                execute: async () =>
                {
                    var data = (irrigations: Array.Empty<IrrigationModel>(), measurements: Array.Empty<MeasurementModel>());
                    var startDate = DateTimeOffset.UtcNow.AddDays(1);
                    var endDate = startDate.AddDays(-1 * SearchOptions - 1);
                    var ids = Nodes!.Select(x => x.Id).ToHashSet();

                    Console.WriteLine(SearchOptions.Days);

                    var irrigations = nodeService.GetIrrigations(new SearchManyIrrigations(ids, startDate, endDate));
                    var measurements = nodeService.GetMeasurements(new SearchManyMeasurements(ids, startDate, endDate));

                    await Task.WhenAll(irrigations, measurements);

                    irrigations.Result.Switch(s => data.irrigations = s, e => Console.WriteLine(e));
                    measurements.Result.Switch(s => data.measurements = s, e => Console.WriteLine(e));

                    NodeData = data;
                });

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Observable
                    .FromAsync(async () => await groupService.Get(groupId).SwitchAsync(
                        s => groupService.Watch(s.Id).BindTo(this, vm => vm.Group),
                        e => Console.WriteLine(e)))
                    .Subscribe(_ =>
                    {
                        NotFound = Group is null;
                        Loading = false;

                        if (NotFound is false)
                            LoadNodes.Execute().Subscribe();
                    });
            });
        }
    }
}