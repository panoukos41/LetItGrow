using LetItGrow.CoreHost.Services;
using LetItGrow.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    public static partial class DependencyInjection
    {
        /// <summary>
        /// Add CORE services:<br/>
        /// <br/>
        /// - AddHttpContextAccessor()<br/>
        /// - AddSingleton(<see cref="IClockService"/>, <see cref="ClockService"/>)<br/>
        /// - AddSingleton(<see cref="IPrimaryKeyService"/>, <see cref="PrimaryKeyService"/>)<br/>
        /// - AddSingleton(<see cref="ITokenService"/>, <see cref="TokenService"/>)<br/>
        /// - AddScoped{<see cref="IUserService"/>, <see cref="UserService"/>)<br/>
        /// </summary>
        public static IServiceCollection AddCoreHostServices(this IServiceCollection services)
        {
            // Service that helps to access the user id.
            services.AddHttpContextAccessor();

            services.AddSingleton<IClockService, ClockService>();
            services.AddSingleton<IPrimaryKeyService, PrimaryKeyService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}