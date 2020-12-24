namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// Indicates that an entity has a concurrencystamp property.
    /// </summary>
    public interface IConcurrencyStamp
    {
        /// <summary>
        /// A random value that must change whenever an object is persisted to the store.<br/>
        /// This is usually generated in the database.
        /// </summary>
        uint ConcurrencyStamp { get; init; }
    }
}