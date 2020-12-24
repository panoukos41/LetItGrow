using NodaTime;

namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// Indicates that an entity has a createdat property.
    /// </summary>
    public interface ICreatedAt
    {
        /// <summary>
        /// The exact momment this entity was created.
        /// </summary>
        public Instant CreatedAt { get; }
    }
}