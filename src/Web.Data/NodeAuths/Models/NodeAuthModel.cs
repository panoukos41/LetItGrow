using NodaTime;

namespace LetItGrow.Web.Data.NodeAuths.Models
{
    public record NodeAuthModel
    {
        public NodeAuthModel()
        {
            NodeId = string.Empty;
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
        }

        /// <summary>
        /// The id of the node that this auth belongs to.
        /// </summary>
        public string NodeId { get; init; }

        /// <summary>
        /// The token that was generated. This is not null only the first time its created.
        /// </summary>
        public string? Token { get; init; }

        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        public Instant CreatedAt { get; }

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        public Instant UpdatedAt { get; set; }

        /// <summary>
        /// The id of the user that created this entity.
        /// </summary>
        public string CreatedBy { get; }

        /// <summary>
        /// The id of the user that updated this entity.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// A random value that must change whenever an object is persisted to the store.<br/>
        /// This is usually generated in the database.
        /// </summary>
        public uint ConcurrencyStamp { get; }

        /// <summary>
        ///
        /// </summary>
        public bool IsValid { get; init; }
    }
}