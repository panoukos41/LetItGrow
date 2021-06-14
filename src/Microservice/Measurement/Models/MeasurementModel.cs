using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Measurement.Models
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
        public string Id { get; set; }

        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The momment this measurment was measured. This comes from the node.
        /// </summary>
        [JsonPropertyName("measuredAt")]
        public DateTimeOffset MeasuredAt { get; set; }

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        [JsonPropertyName("ariTemperatureC")]
        public double AirTemperatureC { get; set; }

        /// <summary>
        /// The Humidity.
        /// </summary>
        [JsonPropertyName("airHumidity")]
        public double AirHumidity { get; set; }

        /// <summary>
        /// The Moisture of the soil.
        /// </summary>
        [JsonPropertyName("soilMoisture")]
        public double SoilMoisture { get; set; }
    }
}