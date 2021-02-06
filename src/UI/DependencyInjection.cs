using LetItGrow.UI.Services;
using LetItGrow.UI.ViewModels.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LetItGrow.UI
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Add default interfaces and implementations for UI:<br/>
        /// AddSingleton{<see cref="IDashboardService"/>, <see cref="DashboardService"/>}<br/>
        /// AddSingleton{<see cref="IIrrigationService"/>, <see cref="IrrigationService"/>}<br/>
        /// AddSingleton{<see cref="IMeasurementService"/>, <see cref="MeasurementService"/>}<br/>
        /// AddSingleton{<see cref="INodeAuthService"/>, <see cref="NodeAuthService"/>}<br/>
        /// AddSingleton{<see cref="INodeGroupService"/>, <see cref="NodeGroupService"/>}<br/>
        /// AddSingleton{<see cref="INodeService"/>, <see cref="NodeService"/>}<br/>
        /// AddSingleton{<see cref="INotificationService"/>, <see cref="NotificationService"/>();<br/>
        /// <br/>
        /// Add View Models:<br/>
        /// AddScoped{<see cref="HomeViewModel"/>();
        /// </summary>
        public static IServiceCollection AddUI(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services
            services.AddSingleton<IDashboardService, DashboardService>();
            services.AddSingleton<IIrrigationService, IrrigationService>();
            services.AddSingleton<IMeasurementService, MeasurementService>();
            services.AddSingleton<INodeAuthService, NodeAuthService>();
            services.AddSingleton<INodeGroupService, NodeGroupService>();
            services.AddSingleton<INodeService, NodeService>();
            services.AddSingleton<INotificationService, NotificationService>();

            // Add ViewModels
            services.AddScoped<HomeViewModel>();

            return services;
        }
    }
}