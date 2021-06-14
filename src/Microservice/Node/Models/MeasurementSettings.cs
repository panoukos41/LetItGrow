using ProtoBuf;

namespace LetItGrow.Microservice.Node.Models
{
    /// <summary>
    /// Settings for a measurement node.
    /// </summary>
    [ProtoContract]
    public record MeasurementSettings
    {
        /// <summary>
        /// A number in seconds indicating how often a measurement node polls.
        /// It polls whether to stop irrigation. If no connection can be reached it will stop.<br/>
        /// <br/>
        /// Default is 600 => 10 minutes.<br/>
        /// Min is 60      => 1 minute.<br/>
        /// Max is 3600    => 1 hour.
        /// </summary>
        [ProtoMember(1)]
        public int PollInterval { get; set; }

        /// <summary>
        /// Initialize a new <see cref="MeasurementSettings"/> record with default settings.
        /// </summary>
        public MeasurementSettings()
        {
            PollInterval = 600;
        }

        /// <summary>
        /// Initialize a new <see cref="MeasurementSettings"/> record.
        /// </summary>
        public MeasurementSettings(int pollInterval)
        {
            PollInterval = pollInterval;
        }
    }
}