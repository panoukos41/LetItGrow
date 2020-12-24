using NodaTime;

namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// Indicates that an entity has an updatedat property.
    /// </summary>
    public interface IUpdatedAt
    {
        /// <summary>
        /// The exact momment this entity was updated.
        /// </summary>
        public Instant UpdatedAt { get; set; }
    }
}