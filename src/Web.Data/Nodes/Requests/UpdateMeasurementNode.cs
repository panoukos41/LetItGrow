using LetItGrow.Web.Data.Nodes.Models;
using LetItGrow.Web.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Web.Data.Nodes.Requests
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
    }
}