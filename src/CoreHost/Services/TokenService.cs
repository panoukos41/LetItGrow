using LetItGrow.Services;
using System.Buffers;
using System.Security.Cryptography;

namespace LetItGrow.CoreHost.Services
{
    /// <summary>
    /// Implementation of <see cref="ITokenService"/>
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <inheritdoc/>
        public string Create() => Create(45);

        #region Token Implementations

        public const string TokenChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        /// <summary>
        /// Generate any length character string suitable for use as a token.
        /// </summary>
        /// <returns>A string for use as a token.</returns>
        public static string Create(int length)
        {
            var token = ArrayPool<char>.Shared.Rent(length);
            for (int i = 0; i < length; i++)
            {
                var charIndex = RandomNumberGenerator.GetInt32(0, TokenChars.Length);
                token[i] = TokenChars[charIndex];
            }
            return new string(token[..length]);
        }

        #endregion
    }
}