using DynamicData;
using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.UI.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;

namespace LetItGrow.UI.ViewModels.Pages
{
    using GroupedNodes = ReadOnlyObservableCollection<(NodeGroupModel Group, List<NodeModel> Nodes)>;
    using UnGroupedNodes = ReadOnlyObservableCollection<NodeModel>;

    public class HomeViewModel : ReactiveObject
    {
        private readonly INodeService _nodeService;
        private readonly INodeGroupService _groupService;

        private readonly GroupedNodes _groupedNodes;
        private readonly UnGroupedNodes _ungroupedNodes;

        /// <summary>
        /// An observable collection for groups with their nodes.
        /// </summary>
        public GroupedNodes GroupedNodes => _groupedNodes;

        /// <summary>
        /// An observable collection for nodes that have no group.
        /// </summary>
        public UnGroupedNodes UngroupedNodes => _ungroupedNodes;

        public HomeViewModel(INodeService nodeService, INodeGroupService groupService)
        {
            _nodeService = nodeService;
            _groupService = groupService;

            _groupService.Connect()
                .FullJoinMany(
                    right: _nodeService.Connect(x => x.NodeGroupId is not null),
                    rightKeySelector: node => node.NodeGroupId!,
                    resultSelector: (group, nodes) =>
                    {
                        var list = nodes.Items.ToList();
                        return group.HasValue
                            ? (group.Value, list)
                            : (tempGroup(list[0]), list);

                        static NodeGroupModel tempGroup(NodeModel node) => new()
                        {
                            Id = node.Id,
                            Name = node.Id,
                            Description = "This group was most likely made because changes of deleted group have not propagated to the app yet."
                        };
                    })
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _groupedNodes)
                .DisposeMany()
                .Subscribe(x => this.RaisePropertyChanged(nameof(GroupedNodes)));

            _nodeService.Connect()
                .Filter(x => x.NodeGroupId is null)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Bind(out _ungroupedNodes)
                .DisposeMany()
                .Subscribe(x => this.RaisePropertyChanged(nameof(UngroupedNodes)));
        }
    }
}