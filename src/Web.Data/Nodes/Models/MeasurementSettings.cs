using System.Text.Json.Serialization;

namespace LetItGrow.Web.Data.Nodes.Models
{
    /// <summary>
    /// Settings for a measurement node.
    /// </summary>
    public record MeasurementSettings
    {
        /// <summary>
        /// Initialize a new <see cref="MeasurementSettings"/> record with default settings.
        /// </summary>
        public MeasurementSettings()
        {
            PollInterval = 600;
        }

        /// <summary>
        /// A number in seconds indicating how often a measurement node polls.
        /// It polls whether to stop irrigation. If no connection can be reached it will stop.<br/>
        /// <br/>
        /// Default is 600 => 10 minutes.<br/>
        /// Min is 60      => 1 minute.<br/>
        /// Max is 3600    => 1 hour.
        /// </summary>
        [JsonPropertyName("pollInterval")]
        public uint PollInterval { get; init; }
    }
}