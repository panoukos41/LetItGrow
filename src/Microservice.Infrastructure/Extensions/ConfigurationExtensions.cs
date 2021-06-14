using System.Linq;

namespace Microsoft.Extensions.Configuration
{
    public static class ConfigurationExtensions
    {
        public static string? GetCouchDbName(this IConfiguration configuration)
        {
            return configuration
                .GetService("couchdb")
                .Split(';')
                .ElementAtOrDefault(3);
        }
    }
}