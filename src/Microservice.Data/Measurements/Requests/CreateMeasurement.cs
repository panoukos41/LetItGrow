using MediatR;
using NodaTime;

namespace LetItGrow.Microservice.Data.Measurements.Requests
{
    public record CreateMeasurement : IRequest
    {
        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment this measurement was taken.
        /// </summary>
        public Instant MeasuredAt { get; init; }

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        public double AirTemperatureC { get; init; }

        /// <summary>
        /// The Humidity.
        /// </summary>
        public double AirHumidity { get; init; }

        /// <summary>
        /// The Moisture of the soil.
        /// </summary>
        public double SoilMoisture { get; init; }
    }
}