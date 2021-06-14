using CouchDB.Driver.Types;
using Newtonsoft.Json;
using System;

namespace LetItGrow.Microservice.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class Measurement : CouchDocument, IEntity
    {
        /// <summary>
        /// The id of the <see cref="Node"/> that created this measurement.
        /// </summary>
        [JsonProperty("node_id")]
        public string NodeId { get; init; } = string.Empty;

        /// <inheritdoc/>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; init; }

        /// <summary>
        /// The momment this measurment was measured. This comes from the node.
        /// </summary>
        [JsonProperty("measured_at")]
        public DateTimeOffset MeasuredAt { get; init; }

        /// <summary>
        /// The momment this measurment was measured in unix seconds.
        /// </summary>
        [JsonProperty("measured_at_unix")]
        public long MeasuredAtUnix => MeasuredAt.ToUnixTimeSeconds();

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        [JsonProperty("air_temperature_c")]
        public double AirTemperatureC { get; init; }

        /// <summary>
        /// The Humidity.
        /// </summary>
        [JsonProperty("air_humidity")]
        public double AirHumidity { get; init; }

        /// <summary>
        /// The Moisture of the soil.
        /// </summary>
        [JsonProperty("soil_moisture")]
        public double SoilMoisture { get; init; }
    }
}