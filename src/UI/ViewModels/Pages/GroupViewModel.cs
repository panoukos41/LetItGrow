using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.NodeGroups.Models;
using LetItGrow.UI.Services;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace LetItGrow.UI.ViewModels.Pages
{
    public class GroupViewModel : ReactiveObject, IActivatableViewModel
    {
        private readonly INodeGroupService _groupService;
        private NodeGroupModel _Group;
        private bool _LoadingGroup;

        public ViewModelActivator Activator { get; } = new();

        public NodeGroupModel Group
        {
            get => _Group;
            set => this.RaiseAndSetIfChanged(ref _Group, value);
        }

        public bool LoadingGroup
        {
            get => _LoadingGroup;
            set => this.RaiseAndSetIfChanged(ref _LoadingGroup, value);
        }

        public GroupViewModel(string groupId, INodeGroupService groupService)
        {
            _groupService = groupService;
            _Group = new();
            _LoadingGroup = true;

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                _ = HandleActivation(groupId);
            });
        }

        protected Task HandleActivation(string groupId)
        {
            return _groupService
                .Get(groupId)
                .OnSuccess(r =>
                {
                    _groupService
                        .Watch(groupId)
                        .BindTo(this, vm => vm.Group);

                    LoadingGroup = false;
                })
                .AsTask();
        }
    }
}