using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Irrigation.Models;
using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Irrigation.Requests
{
    public record SearchIrrigations : BaseSearch<IrrigationModel>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("from")]
        public DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("to")]
        public DateTimeOffset EndDate { get; set; }
    }
}