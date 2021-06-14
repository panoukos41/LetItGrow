using NodaTime;
using ProtoBuf;

namespace Node.Data
{
    [ProtoContract]
    public class Measurement
    {
        /// <summary>
        /// The id of the Node that created this measurement.
        /// </summary>
        [ProtoMember(1)]
        public string NodeId { get; set; } = string.Empty;

        /// <summary>
        /// The exact momment this measurement was taken.
        /// </summary>
        [ProtoMember(2)]
        public Instant MeasuredAt { get; set; }

        /// <summary>
        /// The temperature in Celsius.
        /// </summary>
        [ProtoMember(3)]
        public double AirTemperatureC { get; set; }

        /// <summary>
        /// The Humidity.
        /// </summary>
        [ProtoMember(4)]
        public double AirHumidity { get; set; }

        /// <summary>
        /// The Moisture of the soil.
        /// </summary>
        [ProtoMember(5)]
        public double SoilMoisture { get; set; }
    }
}