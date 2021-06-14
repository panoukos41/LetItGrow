using CouchDB.Driver.Types;
using LetItGrow.Microservice.Entities.Models;
using LetItGrow.Microservice.Group.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LetItGrow.Microservice.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class Group : CouchDocument, IEntity, IAuditableEntity
    {
        /// <summary>
        /// This group's type.
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public GroupType Type { get; set; }

        /// <summary>
        /// A name for the group.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// A small description for the group.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// Audit info about an entity.
        /// </summary>
        [JsonProperty("audit")]
        public AuditInfo Audit { get; set; } = new();
    }
}