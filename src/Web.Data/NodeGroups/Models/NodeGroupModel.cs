using LetItGrow.Web.Data.Nodes.Models;
using NodaTime;
using System.Collections.Generic;

namespace LetItGrow.Web.Data.NodeGroups.Models
{
    /// <summary>
    /// The model for a node group.
    /// </summary>
    public record NodeGroupModel
    {
        /// <summary>
        /// The group's unique id.
        /// </summary>
        public string Id { get; init; } = string.Empty;

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        public Instant CreatedAt { get; init; }

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        public Instant UpdatedAt { get; init; }

        /// <summary>
        /// The id of the user that created this entity.
        /// </summary>
        public string CreatedBy { get; init; } = string.Empty;

        /// <summary>
        /// The id of the user that updated this entity.
        /// </summary>
        public string UpdatedBy { get; init; } = string.Empty;

        /// <summary>
        /// A value to indicate if the data has changed since last retreived.
        /// </summary>
        public uint ConcurrencyStamp { get; init; }

        /// <summary>
        /// A name for the group.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// A small description for the group.
        /// </summary>
        public string? Description { get; init; }
    }
}