namespace LetItGrow.Services
{
    /// <summary>
    /// A service to grab the current user in the request pipeline.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get the identity of the user or null if not found id is found.
        /// </summary>
        /// <returns>The user identity or null.</returns>
        string? GetId();
    }
}