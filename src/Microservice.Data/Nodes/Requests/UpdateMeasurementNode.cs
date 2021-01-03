using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to update a Measurement node.
    /// </summary>
    public record UpdateMeasurementNode : UpdateNodeBase, IRequest<NodeModelUpdate>
    {
        /// <summary>
        /// The new settings for the node.
        /// </summary>
        public MeasurementSettings? Settings { get; init; }

        public UpdateMeasurementNode() : base()
        {
        }

        public UpdateMeasurementNode(NodeModel node) : base(node)
        {
        }

        public UpdateMeasurementNode(NodeModel node, MeasurementSettings settings) : base(node)
        {
            Settings = settings;
        }
    }
}