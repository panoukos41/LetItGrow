using CouchDB.Driver.Types;
using LetItGrow.Microservice.Entities.Models;
using LetItGrow.Microservice.Node.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace LetItGrow.Microservice.Entities
{
    /// <summary>
    /// The base recrod for all nodes.
    /// </summary>
    public class Node : CouchDocument, IEntity, IAuditableEntity
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the node. (optional)
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// This node's type.
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public NodeType Type { get; set; }

        /// <summary>
        /// The token that was generated.
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Audit info about an entity.
        /// </summary>
        [JsonProperty("audit")]
        public AuditInfo Audit { get; set; } = new();

        /// <summary>
        /// A nodes settings.
        /// </summary>
        [JsonProperty("settings")]
        public JObject? Settings { get; set; }

        /// <summary>
        /// A foreign key that points to the node's group.
        /// </summary>
        [JsonProperty("group_id")]
        public string? GroupId { get; set; }

        /// <summary>
        /// A flag indicating if the node is connected or not.
        /// </summary>
        [JsonProperty("connected")]
        public bool Connected { get; set; }
    }
}