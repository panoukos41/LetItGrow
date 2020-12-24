using System;
using System.Text;

namespace Microsoft.Extensions.Configuration
{
    public static partial class ConfigurationExtensions
    {
        /// <summary>
        /// Get the Name property of the Application section.
        /// </summary>
        /// <param name="configuration">The configuration to search for the value.</param>
        /// <returns>Returns the application name.</returns>
        public static string GetApplicationName(this IConfiguration configuration)
        {
            return configuration.ReadValue("application:name");
        }

        /// <summary>
        /// Get the Version property of the Application section.
        /// </summary>
        /// <param name="configuration">The configuration to search for the value.</param>
        /// <returns>Returns the application name.</returns>
        public static string GetApplicationVersion(this IConfiguration configuration)
        {
            return configuration.ReadValue("application:version");
        }

        /// <summary>
        /// Get whether swagger should be enabled or not.
        /// </summary>
        /// <param name="configuration">The configuration to search for the value.</param>
        /// <returns>True to enable swagger otherwise false.</returns>
        public static bool GetSwagger(this IConfiguration configuration)
        {
            if (bool.TryParse(configuration["swagger:enabled"], out var enabled))
            {
                return enabled;
            }

            return false;
        }

        /// <summary>
        /// Get a service uri.
        /// </summary>
        /// <param name="configuration">The configuration to search for values.</param>
        /// <param name="key">The key of the service to search for.</param>
        /// <returns>The complete uri for the service.</returns>
        public static Uri GetService(this IConfiguration configuration, string key)
        {
            var value = configuration.ReadValue($"services:{key}");
            return new Uri(value);
        }

        /// <summary>
        /// Get the connection and schema values of the database for the specified key.
        /// If no key is specified then connection and schema will be searched under the
        /// database section.
        /// </summary>
        /// <param name="configuration">The configuration to search for values.</param>
        /// <param name="key">The key to use for search.</param>
        /// <returns>The connection string and the schema to use.</returns>
        public static (string connection, string schema) GetDatabase(this IConfiguration configuration, string? key = null)
        {
            const string section = "databases";
            bool keyless = key is null;

            var connection = keyless
                ? configuration.ReadValue($"{section}:connection")
                : configuration.ReadValue($"{section}:{key}:connection");

            var schema = keyless
                ? configuration.ReadValue($"{section}:schema")
                : configuration.ReadValue($"{section}:{key}:schema");

            return (connection, schema);
        }

        /// <summary>
        /// Get a value from the Secrets section.
        /// </summary>
        /// <param name="configuration">The configuration to search for values.</param>
        /// <param name="key">The secret to search for.</param>
        /// <returns>The secret.</returns>
        public static string GetSecret(this IConfiguration configuration, string key)
        {
            return configuration.ReadValue($"secrets:{key}");
        }

        /// <summary>
        /// Get a value from the Secrets section as byte[] using the provided encoding.
        /// If no encoding is provided <see cref="Encoding.UTF8"/> will be used.
        /// </summary>
        /// <param name="configuration">The configuration to search for values.</param>
        /// <param name="key">The secret to search for.</param>
        /// <param name="encoding">The encoding to use, pass null or default to use UTF8.</param>
        /// <returns>The secret in byte[] form.</returns>
        public static byte[] GetSecret(this IConfiguration configuration, string key, Encoding? encoding = null)
        {
            var secret = configuration.GetSecret(key);

            return encoding == null
                    ? Encoding.UTF8.GetBytes(secret)
                    : encoding.GetBytes(secret);
        }

        private static string ReadValue(this IConfiguration configuration, string key)
        {
            var value = configuration[key] ??
                throw new ArgumentNullException(nameof(key), $"Could not find value for: '{key}'");

            if (value.StartsWith('<'))
            {
                throw new ArgumentException($"No value has been provided for '{key}'", nameof(key));
            }

            return value;
        }
    }
}