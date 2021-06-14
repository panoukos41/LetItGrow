using LetItGrow.UI.Common.Services;
using LetItGrow.UI.Common.ViewModels;
using LetItGrow.UI.Group.Services;
using LetItGrow.UI.Node.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LetItGrow.UI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add default interfaces and implementations for UI:<br/>
        /// AddSingleton{<see cref="HubService"/>}<br/>
        /// AddSingleton{<see cref="IGroupService"/>, <see cref="GroupService"/>}<br/>
        /// AddSingleton{<see cref="INodeService"/>, <see cref="NodeService"/>}<br/>
        /// <br/>
        /// Add View Models:<br/>
        /// AddScoped{<see cref="HomeViewModel"/>();
        /// </summary>
        public static IServiceCollection AddUI(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services
            services.AddSingleton<HubService>();
            services.AddSingleton<IGroupService, GroupService>();
            services.AddSingleton<INodeService, NodeService>();

            // Add ViewModels
            services.AddScoped<HomeViewModel>();

            return services;
        }
    }
}