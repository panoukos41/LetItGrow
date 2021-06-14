using DynamicData;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.UI.Group.Services;
using LetItGrow.UI.Node.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace LetItGrow.UI.Common.ViewModels
{
    using GroupedNodes = ReadOnlyObservableCollection<(GroupModel Group, List<NodeModel> Nodes)>;
    using UnGroupedNodes = ReadOnlyObservableCollection<NodeModel>;

    public class HomeViewModel : RxViewModel
    {
        private readonly GroupedNodes _groupedNodes;
        private readonly UnGroupedNodes _ungroupedNodes;

        public GroupedNodes GroupedNodes => _groupedNodes;

        public UnGroupedNodes UngroupedNodes => _ungroupedNodes;

        public HomeViewModel(INodeService nodeService, IGroupService groupService)
        {
            groupService.Connect()
                .FullJoinMany(
                right: nodeService.Connect(x => x.GroupId is not null),
                rightKeySelector: node => node.GroupId!,
                resultSelector: (group, nodes) =>
                {
                    var list = nodes.Items.ToList();
                    return group.HasValue
                        ? (group.Value, list)
                        : (tempGroup(list[0]), list);

                    static GroupModel tempGroup(NodeModel node) => new()
                    {
                        Id = node.Id,
                        Name = node.Id,
                        Description = "This group was most likely made because changes of deleted group have not propagated to the whole app yet."
                    };
                })
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _groupedNodes)
                .DisposeMany()
                .Subscribe(x => this.RaisePropertyChanged(nameof(GroupedNodes)));

            nodeService.Connect()
                .Filter(x => x.GroupId is null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ungroupedNodes)
                .DisposeMany()
                .Subscribe(x => this.RaisePropertyChanged(nameof(UngroupedNodes)));
        }
    }
}