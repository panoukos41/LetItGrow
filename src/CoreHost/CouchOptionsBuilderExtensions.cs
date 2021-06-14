using CouchDB.Driver.Options;
using Flurl;
using Microsoft.Extensions.Configuration;
using System;

namespace CouchDB.Driver
{
    public static class CouchOptionsBuilderExtensions
    {
        /// <summary>
        /// Configure couchdb from the configuration.<br/>
        /// Configures the http endoint, the name and the password.
        /// </summary>
        /// <param name="builder">The couchdb builder to configure.</param>
        /// <param name="configuration">The configuration to use.</param>
        /// <returns>The builder for more customization.</returns>
        public static CouchOptionsBuilder Configure(this CouchOptionsBuilder builder, IConfiguration configuration)
        {
            var values = configuration.GetService("couchdb").Split(';');

            if (values.Length < 3) throw new ArgumentException(
                $"Invalid configuration value. The connection string needs to be at least 3 values seperated by ';' you provided {values.Length}");

            var username = values[0];
            var password = values[1];
            var endpoint = new Url(values[2]);

            builder.UseBasicAuthentication(username, password)
                   .UseEndpoint(endpoint);

            return builder;
        }

        /// <summary>
        /// Get the couchdb database to use from the configuration.<br/>
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <returns>The builder for more customization.</returns>
        public static string GetCouchDatabase(this IConfiguration configuration)
        {
            var values = configuration.GetService("couchdb").Split(';');

            if (values.Length < 4) throw new ArgumentException(
                $"Invalid configuration value. The connection string needs to be at least 4 values seperated by ';' " +
                $"where the fourht value is the database name. You provided {values.Length}");

            return values[3];
        }
    }
}