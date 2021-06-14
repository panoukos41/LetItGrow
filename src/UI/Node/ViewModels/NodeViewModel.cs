using LetItGrow.Microservice.Common;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using LetItGrow.UI.Common;
using LetItGrow.UI.Node.Services;
using OneOf;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LetItGrow.UI.Node.ViewModels
{
    public class NodeViewModel : RxViewModel
    {
        [Reactive]
        public NodeModel Node { get; set; }

        [Reactive]
        public OneOf<IrrigationModel[], MeasurementModel[]> Data { get; set; }

        [Reactive]
        public SearchOptions SearchOptions { get; set; }

        public ReactiveCommand<Unit, Result<RefreshModel>> RefreshToken { get; }

        public ReactiveCommand<Unit, Unit> LoadData { get; }

        public NodeViewModel(string nodeId, INodeService nodeService)
        {
            Loading = true;
            Node = null!;
            SearchOptions = new();

            RefreshToken = ReactiveCommand.CreateFromTask(() =>
                nodeService.Refresh(new RefreshToken(Node)));

            LoadData = ReactiveCommand.CreateFromTask(() => Node.Type switch
            {
                NodeType.Irrigation => nodeService.GetIrrigations(
                    new SearchIrrigations
                    {
                        NodeId = Node.Id,
                        StartDate = DateTimeOffset.UtcNow.AddDays(1),
                        EndDate = DateTimeOffset.UtcNow.AddDays(-1 * SearchOptions - 1)
                    }).SwitchAsync(s => Data = s, e => Console.WriteLine(e)),

                NodeType.Measurement => nodeService.GetMeasurements(
                    new SearchMeasurements
                    {
                        NodeId = Node.Id,
                        StartDate = DateTimeOffset.UtcNow.AddDays(1),
                        EndDate = DateTimeOffset.UtcNow.AddDays(-1 * SearchOptions - 1)
                    }).SwitchAsync(s => Data = s, e => Console.WriteLine(e)),

                _ => Task.CompletedTask,
            });

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                Observable
                    .FromAsync(async () => await nodeService.Get(nodeId).SwitchAsync(
                        s => nodeService.Watch(s.Id).BindTo(this, vm => vm.Node),
                        e => Console.WriteLine(e)))
                    .Subscribe(_ =>
                    {
                        NotFound = Node is null;
                        Loading = false;
                    });
            });
        }
    }
}