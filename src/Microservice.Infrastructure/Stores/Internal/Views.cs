using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LetItGrow.Microservice.Stores.Internal
{
    using Group = Entities.Group;
    using Irrigation = Entities.Irrigation;
    using Measurement = Entities.Measurement;
    using Node = Entities.Node;

    public static class Views
    {
        public const string Design = "letitgrow";

        public static View<string, string, Node> Node { get; set; } =
            new(Design, "node");

        public static View<string, string, Node> Node_GroupId { get; set; } =
            new(Design, "node.group_id");

        public static View<string, string, Group> Group { get; set; } =
            new(Design, "group");

        public static View<IrrigationViewKey, string, Irrigation> Irrigations { get; set; } =
            new(Design, "irrigation.issued_at_unix");

        public class IrrigationViewKey : ViewKey
        {
            [JsonIgnore]
            public string NodeId { get; }

            [JsonIgnore]
            public DateTimeOffset IssuedAt { get; }

            public IrrigationViewKey(string nodeId, DateTimeOffset issuedAt) =>
                (NodeId, IssuedAt) = (nodeId, issuedAt);

            [JsonConstructor]
            private IrrigationViewKey(IList<object> list)
            {
                NodeId = (string)list[0];
                IssuedAt = DateTimeOffset.FromUnixTimeSeconds((long)list[1]).ToUniversalTime();
            }

            public override object[] Key => new object[]
            {
                NodeId,
                IssuedAt.ToUnixTimeSeconds()
            };
        }

        public static View<MeasurementViewKey, string, Measurement> Measurements { get; set; } =
            new(Design, "measurement.measured_at_unix");

        public class MeasurementViewKey : ViewKey
        {
            [JsonIgnore]
            public string NodeId { get; }

            [JsonIgnore]
            public DateTimeOffset MeasuredAt { get; }

            public MeasurementViewKey(string nodeId, DateTimeOffset measuredAt) =>
                (NodeId, MeasuredAt) = (nodeId, measuredAt);

            [JsonConstructor]
            private MeasurementViewKey(IList<object> list)
            {
                NodeId = (string)list[0];
                MeasuredAt = DateTimeOffset.FromUnixTimeSeconds((long)list[1]).ToUniversalTime();
            }

            public override object[] Key => new object[]
            {
                NodeId,
                MeasuredAt.ToUnixTimeSeconds(),
            };
        }
    }
}