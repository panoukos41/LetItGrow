using LetItGrow.Services;
using System.IO;

namespace LetItGrow.CoreHost.Services
{
    /// <summary>
    /// Implementation of <see cref="IPrimaryKeyService"/>
    /// </summary>
    public class PrimaryKeyService : IPrimaryKeyService
    {
        /// <inheritdoc/>
        public string Create() =>
            // Removes "." from the string befor it returns.
            Path.GetRandomFileName().Remove(8, 1);
    }
}