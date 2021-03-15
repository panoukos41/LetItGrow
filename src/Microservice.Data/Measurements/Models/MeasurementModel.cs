using NodaTime;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("id")]
        public string Id { get; init; }

        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; }

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public Instant CreatedAt { get; init; }

        /// <summary>
        /// The momment this measurment was measured. This comes from the node.
        /// </summary>
        [JsonPropertyName("measuredAt")]
        public Instant MeasuredAt { get; }

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        [JsonPropertyName("ariTemperatureC")]
        public double AirTemperatureC { get; init; }

        /// <summary>
        /// The Humidity.
        /// </summary>
        [JsonPropertyName("airHumidity")]
        public double AirHumidity { get; init; }

        /// <summary>
        /// The Moisture of the soil.
        /// </summary>
        [JsonPropertyName("soilMoisture")]
        public double SoilMoisture { get; init; }
    }
}