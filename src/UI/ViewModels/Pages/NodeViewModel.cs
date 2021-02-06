using LetItGrow.Microservice.Data;
using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.UI.Services;
using ReactiveUI;
using System.Reactive.Disposables;
using System.Threading.Tasks;

namespace LetItGrow.UI.ViewModels.Pages
{
    public class NodeViewModel : ReactiveObject, IActivatableViewModel
    {
        private readonly INodeService _nodeService;
        private NodeModel _Node;
        private bool _LoadingNode;

        public ViewModelActivator Activator { get; } = new();

        /// <summary>
        /// The node to display.
        /// </summary>
        public NodeModel Node
        {
            get => _Node;
            set => this.RaiseAndSetIfChanged(ref _Node, value);
        }

        /// <summary>
        /// Indicates wheter the node is loaded or not.
        /// </summary>
        public bool LoadingNode
        {
            get => _LoadingNode;
            set => this.RaiseAndSetIfChanged(ref _LoadingNode, value);
        }

        public NodeViewModel(string nodeId, INodeService nodeService)
        {
            _nodeService = nodeService;
            _Node = new();
            _LoadingNode = true;

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                _ = HandleActivation(nodeId);
            });
        }

        protected async Task HandleActivation(string nodeId)
        {
            await _nodeService
                .Get(nodeId)
                .OnSuccess(result =>
                {
                    _nodeService
                        .Watch(result.Id)
                        .BindTo(this, x => x.Node);

                    LoadingNode = false;
                });
        }
    }
}