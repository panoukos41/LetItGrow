using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Data.Nodes.Models
{
    /// <summary>
    /// The model for an irrigation node.
    /// </summary>
    public record IrrigationNodeModel : NodeModel
    {
        /// <summary>
        /// The specific settings of an IrrigationNode.
        /// </summary>
        [JsonPropertyName("settings")]
        public IrrigationSettings? Settings { get; init; }
    }
}