using Newtonsoft.Json;
using System;

namespace LetItGrow.Microservice.Entities.Models
{
    public class AuditInfo
    {
        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The user id that created this entity.
        /// </summary>
        [JsonProperty("created_by")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        /// <summary>
        /// The user id that updated this entity.
        /// </summary>
        [JsonProperty("updated_by")]
        public string UpdatedBy { get; set; } = string.Empty;
    }
}