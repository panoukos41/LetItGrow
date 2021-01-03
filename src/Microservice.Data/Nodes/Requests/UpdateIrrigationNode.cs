using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to update an Irrigation node.
    /// </summary>
    public record UpdateIrrigationNode : UpdateNodeBase, IRequest<NodeModelUpdate>
    {
        /// <summary>
        /// The new settings for the node.
        /// </summary>
        public IrrigationSettings? Settings { get; init; }

        public UpdateIrrigationNode() : base()
        {
        }

        public UpdateIrrigationNode(NodeModel node) : base(node)
        {
        }

        public UpdateIrrigationNode(NodeModel node, IrrigationSettings settings) : base(node)
        {
            Settings = settings;
        }
    }
}