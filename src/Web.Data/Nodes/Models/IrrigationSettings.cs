using System.Text.Json.Serialization;

namespace LetItGrow.Web.Data.Nodes.Models
{
    /// <summary>
    /// Settings for an irrigation node.
    /// </summary>
    public record IrrigationSettings
    {
        /// <summary>
        /// Initialize a new <see cref="IrrigationSettings"/> record with default settings.
        /// </summary>
        public IrrigationSettings()
        {
            PollInterval = 1800;
        }

        /// <summary>
        /// A number in seconds indicating how often an Irrigation node polls.
        /// It polls new measurements and sends them to the server.<br/>
        /// <br/>
        /// Default is 1800 => 30 minutes.<br/>
        /// Min is 60       => 1 minute.<br/>
        /// Max is 21600    => 6 hours.
        /// </summary>
        [JsonPropertyName("pollInterval")]
        public uint PollInterval { get; init; }
    }
}