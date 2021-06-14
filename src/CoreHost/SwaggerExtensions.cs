using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Microsoft.AspNetCore.Hosting
{
    public static class SwaggerExtensions
    {
        private static bool _use = false;
        private static string _name = string.Empty;
        private static string _version = string.Empty;

        public static IServiceCollection TryAddSwagger(this IServiceCollection services, string name, string version, IConfiguration configuration)
        {
            if (!configuration.GetSwagger()) return services;

            _use = true;
            _name = name;
            _version = version;

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

        public static IApplicationBuilder TryUseSwagger(this IApplicationBuilder app)
        {
            if (!_use) return app;

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: $"v{_version}/swagger.json",
                    name: $"{_name} - {_version}");
            });

            return app;
        }
    }
}