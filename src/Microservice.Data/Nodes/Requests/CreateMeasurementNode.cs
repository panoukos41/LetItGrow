using LetItGrow.Microservice.Data.Nodes.Models;
using LetItGrow.Microservice.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Microservice.Data.Nodes.Requests
{
    /// <summary>
    /// A request to create a new Measurement node.
    /// </summary>
    public record CreateMeasurementNode : CreateNodeBase, IRequest<MeasurementNodeModel>
    {
        /// <summary>
        /// The nodes settings.
        /// </summary>
        public MeasurementSettings Settings { get; set; }

        public CreateMeasurementNode()
        {
            Settings = new();
        }

        public CreateMeasurementNode(MeasurementSettings settings)
        {
            Settings = settings;
        }
    }
}