using NodaTime;

namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// An entity that can be edited so it is audited.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        public Instant CreatedAt { get; set; }

        /// <summary>
        /// The user id that created this entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        public Instant UpdatedAt { get; set; }

        /// <summary>
        /// The user id that updated this entity.
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}