using CouchDB.Driver.Types;
using LetItGrow.Microservice.Irrigation.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace LetItGrow.Microservice.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class Irrigation : CouchDocument, IEntity
    {
        /// <summary>
        /// The id of the <see cref="Node"/> that this irrigation was created for.
        /// </summary>
        [JsonProperty("node_id")]
        public string NodeId { get; init; } = string.Empty;

        /// <summary>
        /// The momment this command was issued. This comes from the client.
        /// </summary>
        [JsonProperty("issued_at")]
        public DateTimeOffset IssuedAt { get; init; }

        /// <summary>
        /// The momment this command was issued in unix seconds.
        /// </summary>
        [JsonProperty("issued_at_unix")]
        public long IssuedAtUnix => IssuedAt.ToUnixTimeSeconds();

        /// <summary>
        /// The type of this irrigation request.
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public IrrigationType Type { get; init; }

        /// <inheritdoc/>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; init; }
    }
}