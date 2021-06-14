using LetItGrow.Microservice.Common.Requests;
using MediatR;
using ProtoBuf;
using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Measurement.Requests
{
    public record CreateMeasurement : BaseCreate<Unit>
    {
        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        [ProtoMember(1)]
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        /// <summary>
        /// The exact momment this measurement was taken.
        /// </summary>
        [ProtoMember(2)]
        [JsonPropertyName("measuredAt")]
        public DateTimeOffset MeasuredAt { get; set; }

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        [ProtoMember(3)]
        [JsonPropertyName("airTemperatureC")]
        public double AirTemperatureC { get; set; }

        /// <summary>
        /// The Humidity.
        /// </summary>
        [ProtoMember(4)]
        [JsonPropertyName("airHumidity")]
        public double AirHumidity { get; set; }

        /// <summary>
        /// The Moisture of the soil.
        /// </summary>
        [ProtoMember(5)]
        [JsonPropertyName("soilMoisture")]
        public double SoilMoisture { get; set; }
    }
}