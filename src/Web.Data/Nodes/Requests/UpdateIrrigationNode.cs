using LetItGrow.Web.Data.Nodes.Models;
using LetItGrow.Web.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Web.Data.Nodes.Requests
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
    }
}