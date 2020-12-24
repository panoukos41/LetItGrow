using System.IO;

namespace LetItGrow.Services
{
    /// <summary>
    /// todo: summary
    /// </summary>
    public interface IPrimaryKeyService
    {
        /// <summary>
        /// Generates an 11 character string suitable for use as a primary key.
        /// </summary>
        /// <returns>A string for use as a primary key.</returns>
        string GetNew() =>
            // Removes "." from the string befor it returns.
            Path.GetRandomFileName().Remove(8, 1);
    }
}