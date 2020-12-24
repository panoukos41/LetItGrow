namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// Indicates that an entity has a primary key.
    /// </summary>
    public interface IPrimaryKey
    {
        /// <summary>
        /// The primary key used to identify this object.
        /// </summary>
        string Id { get; }
    }
}