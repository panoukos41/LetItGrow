using System.IO;

namespace LetItGrow.Services
{
    /// <summary>
    /// A service to generate 11 character primary keys.
    /// </summary>
    public interface IPrimaryKeyService
    {
        /// <summary>
        /// Generates an 11 character string suitable for use as a primary key.
        /// </summary>
        /// <returns>A 11 character string for use as a primary key.</returns>
        string Create();
    }
}