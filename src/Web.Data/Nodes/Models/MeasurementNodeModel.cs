using System.Text.Json.Serialization;

namespace LetItGrow.Web.Data.Nodes.Models
{
    /// <summary>
    /// The model for a measurement node.
    /// </summary>
    public record MeasurementNodeModel : NodeModel
    {
        /// <summary>
        /// The specific settings of an MeasurementNode.
        /// </summary>
        [JsonPropertyName("settings")]
        public MeasurementSettings? Settings { get; init; }
    }
}