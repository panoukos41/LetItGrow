namespace LetItGrow.Domain.Interfaces
{
    /// <summary>
    /// Indicates that an entity can be updated from a user.
    /// </summary>
    public interface IUpdatedBy
    {
        /// <summary>
        /// The user id that updated this entity.
        /// </summary>
        public string UpdatedBy { get; set; }
    }
}