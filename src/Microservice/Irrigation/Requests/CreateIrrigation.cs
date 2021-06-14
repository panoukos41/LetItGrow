using LetItGrow.Microservice.Common.Requests;
using LetItGrow.Microservice.Irrigation.Models;
using MediatR;
using ProtoBuf;
using System;
using System.Text.Json.Serialization;

namespace LetItGrow.Microservice.Irrigation.Requests
{
    [ProtoContract]
    public record CreateIrrigation : BaseCreate<Unit>
    {
        /// <summary>
        /// The id of the Node that this irrigation was created for.
        /// </summary>
        [ProtoMember(1)]
        [JsonPropertyName("nodeId")]
        public string NodeId { get; set; } = string.Empty;

        /// <summary>
        /// The exact momment in time this irrigation was created.
        /// </summary>
        [ProtoMember(2)]
        [JsonPropertyName("issuedAt")]
        public DateTimeOffset IssuedAt { get; set; }

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        [ProtoMember(3)]
        [JsonPropertyName("type"), JsonConverter(typeof(JsonStringEnumConverter))]
        public IrrigationType Type { get; set; }
    }
}