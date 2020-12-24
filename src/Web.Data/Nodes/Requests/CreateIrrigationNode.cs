using LetItGrow.Web.Data.Nodes.Models;
using LetItGrow.Web.Data.Nodes.Requests.Internal;
using MediatR;

namespace LetItGrow.Web.Data.Nodes.Requests
{
    /// <summary>
    /// A request to create a new irrigation node.
    /// </summary>
    public record CreateIrrigationNode : CreateNodeBase, IRequest<IrrigationNodeModel>
    {
        /// <summary>
        /// The nodes settings.
        /// </summary>
        public IrrigationSettings? Settings { get; init; }
    }
}