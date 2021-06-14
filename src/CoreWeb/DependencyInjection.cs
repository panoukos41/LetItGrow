using LetItGrow.CoreWeb.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWasm
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreWeb(this IServiceCollection services)
        {
            services.AddSingleton<ThemeJs>();

            return services;
        }
    }
}