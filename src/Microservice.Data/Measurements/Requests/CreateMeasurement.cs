using MediatR;
using NodaTime;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Measurements.Requests
{
    public record CreateMeasurement : IRequest
    {
        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment this measurement was taken.
        /// </summary>
        [JsonPropertyName("measuredAt")]
        public Instant MeasuredAt { get; init; }

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        [JsonPropertyName("airTemperatureC")]
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