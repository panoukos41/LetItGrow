using LetItGrow.Web.Data.Nodes.Models;
using LetItGrow.Web.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Web.Data.Nodes.Requests
{
    /// <summary>
    /// A request to create a new Measurement node.
    /// </summary>
    public record CreateMeasurementNode : CreateNodeBase, IRequest<MeasurementNodeModel>
    {
        /// <summary>
        /// The nodes settings.
        /// </summary>
        public MeasurementSettings? Settings { get; init; }
    }
}