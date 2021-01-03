using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to create a new irrigation node.
    /// </summary>
    public record CreateIrrigationNode : CreateNodeBase, IRequest<IrrigationNodeModel>
    {
        /// <summary>
        /// The nodes settings.
        /// </summary>
        public IrrigationSettings Settings { get; set; }

        public CreateIrrigationNode()
        {
            Settings = new();
        }

        public CreateIrrigationNode(IrrigationSettings settings)
        {
            Settings = settings;
        }
    }
}