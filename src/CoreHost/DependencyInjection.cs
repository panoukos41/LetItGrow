using LetItGrow.CoreHost.Services;
using LetItGrow.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace LetItGrow.CoreHost
{
    public static partial class DependencyInjection
    {
        /// <summary>
        /// Add core services:<br/>
        /// - AddHttpContextAccessor()<br/>
        /// - AddSingleton{<see cref="IClockService"/>, <see cref="ClockService"/>}<br/>
        /// - AddSingleton{<see cref="IPrimaryKeyService"/>, <see cref="PrimaryKeyService"/>}<br/>
        /// - AddSingleton{<see cref="ITokenService"/>, <see cref="TokenService"/>}<br/>
        /// - AddScoped{<see cref="IUserService"/>, <see cref="UserService"/>}()<br/>
        /// </summary>
        public static IServiceCollection AddCoreHostServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Service that helps to access the user id.
            services.AddHttpContextAccessor();

            services.AddSingleton<IClockService, ClockService>();
            services.AddSingleton<IPrimaryKeyService, PrimaryKeyService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        /// <summary>
        /// Add redis related services:<br/>
        /// - AddSingleton{<see cref="IDistributedCache"/>, <see cref="RedisService"/>
        /// - AddSingleton{<see cref="IPubSubService"/>, <see cref="RedisService"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddRedisServices(this IServiceCollection services, IConfiguration configuration)
        {
            var options = ConfigurationOptions.Parse(configuration.GetService("redis"));

            services.AddSingleton(x => new RedisService(options))
                    .AddSingleton<IDistributedCache>(x => x.GetRequiredService<RedisService>())
                    .AddSingleton<IPubSubService>(x => x.GetRequiredService<RedisService>());

            return services;
        }
    }
}