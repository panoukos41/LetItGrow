using LetItGrow.Web.Data.Irrigations.Models;
using MediatR;
using NodaTime;

namespace LetItGrow.Web.Data.Irrigations.Requests
{
    public class CreateIrrigation : IRequest
    {
        public CreateIrrigation()
        {
            NodeId = string.Empty;
        }

        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        public string NodeId { get; init; }

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        public Instant IssuedAt { get; init; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        public IrrigationType Type { get; init; }
    }
}