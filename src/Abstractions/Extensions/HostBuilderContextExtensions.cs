using System;

namespace Microsoft.Extensions.Hosting
{
    public static partial class HostBuilderContextExtensions
    {
        /// <summary>
        /// Gets the absolute path to the directory that contains the application content files.
        /// </summary>
        /// <param name="context">The builder context.</param>
        /// <returns>The absolute path to the directory that contains the application content files.</returns>
        public static string RootPath(this HostBuilderContext context)
        {
            return context.HostingEnvironment.ContentRootPath;
        }

#pragma warning disable IDE0060 // Remove unused parameter

        /// <summary>
        /// Gets the fully qualified path of the current working directory.
        /// </summary>
        /// <param name="context">The builder context.</param>
        /// <returns>The fully qualified path of the current working directory.</returns>
        public static string CurrentPath(this HostBuilderContext context)
        {
            return Environment.CurrentDirectory;
        }

#pragma warning restore IDE0060 // Remove unused parameter
    }
}