namespace LetItGrow.Services
{
    /// <summary>
    /// A service to generate tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generate a 45 character string suitable for use as a token.
        /// </summary>
        /// <returns>A string for use as a token.</returns>
        string Create();
    }
}