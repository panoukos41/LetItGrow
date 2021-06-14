using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Measurement.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Measurement.Requests
{
    public record SearchManyMeasurements : BaseSearch<MeasurementModel>
    {
        public SearchManyMeasurements()
        {
        }

        public SearchManyMeasurements(HashSet<string> nodeIds, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            NodeIds = nodeIds;
            StartDate = startDate;
            EndDate = endDate;
        }

        [JsonPropertyName("nodeIds")]
        public HashSet<string> NodeIds { get; set; } = new();

        [JsonPropertyName("from")]
        public DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("to")]
        public DateTimeOffset EndDate { get; set; }
    }
}