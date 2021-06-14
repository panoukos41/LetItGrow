using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Measurement.Models;
using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Measurement.Requests
{
    public record SearchMeasurements : BaseSearch<MeasurementModel>
    {
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        [JsonPropertyName("from")]
        public DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("to")]
        public DateTimeOffset EndDate { get; set; }
    }
}