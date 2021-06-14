using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static partial class IHostExtensions
    {
        public static T GetRequiredService<T>(this IHost host) where T : notnull =>
            host.Services.GetRequiredService<T>();
    }
}