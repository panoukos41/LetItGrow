namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// Indicates that an entity was created from a user.
    /// </summary>
    public interface ICreatedBy
    {
        /// <summary>
        /// The user id that created this entity.
        /// </summary>
        public string CreatedBy { get; }
    }
}