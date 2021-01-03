using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Microsoft.AspNetCore.Hosting
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection TryAddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            if (!configuration.GetSwagger()) return services;

            var name = configuration.GetApplicationName();
            var version = configuration.GetApplicationVersion();
            var info = new OpenApiInfo
            {
                Version = version,
                Title = name,
            };

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v{version}", info);
            });

            return services;
        }

        public static IApplicationBuilder TryUseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (!configuration.GetSwagger()) return app;

            var name = configuration.GetApplicationName();
            var version = configuration.GetApplicationVersion();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: $"v{version}/swagger.json",
                    name: $"{name} - {version}");
            });

            return app;
        }
    }
}