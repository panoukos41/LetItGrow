using NodaTime;

namespace LetItGrow.Microservice.Data.Measurements.Models
{
    public record MeasurementModel
    {
        public MeasurementModel()
        {
            Id = string.Empty;
            NodeId = string.Empty;
        }

        /// <summary>
        /// The unique id of this measurement.
        /// </summary>
        public string Id { get; init; }

        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        public string NodeId { get; init; }

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        public Instant CreatedAt { get; init; }

        /// <summary>
        /// The momment this measurment was measured. This comes from the node.
        /// </summary>
        public Instant MeasuredAt { get; }

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