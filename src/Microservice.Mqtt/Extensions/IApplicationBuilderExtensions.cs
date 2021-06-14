using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        public static T GetRequiredService<T>(this IApplicationBuilder app) where T : notnull
            => app.ApplicationServices.GetRequiredService<T>();
    }
}