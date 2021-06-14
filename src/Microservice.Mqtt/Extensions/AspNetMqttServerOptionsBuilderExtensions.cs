using Microsoft.Extensions.DependencyInjection;

namespace MQTTnet.AspNetCore
{
    public static partial class AspNetMqttServerOptionsBuilderExtensions
    {
        public static T GetRequiredService<T>(this AspNetMqttServerOptionsBuilder builder) where T : notnull
            => builder.ServiceProvider.GetRequiredService<T>();
    }
}