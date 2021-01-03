using NodaTime;

namespace LetItGrow.Microservice.Data.Irrigations.Models
{
    public record IrrigationModel
    {
        /// <summary>
        /// The id of the irrigation.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        public Instant CreatedAt { get; init; }

        /// <summary>
        /// The momment this command was issued. This comes from the client.
        /// </summary>
        public Instant IssuedAt { get; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        public IrrigationType Type { get; init; }
    }
}