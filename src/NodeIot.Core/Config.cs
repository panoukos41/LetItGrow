using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

#pragma warning disable CA1034 // Nested types should not be visible

namespace LetItGrow.NodeIot
{
    /// <summary>
    /// The applications configuration plus methods to grap configuration
    /// properties faster. Keep in mind that each provided method just calls
    /// get value.
    /// </summary>
    public static class Config
    {
        private static string? clientId;
        private static string? token;
        private static string? server;
        private static string? certificate;
        private static string? type;
        private static bool? test;

        /// <summary>
        ///
        /// </summary>
        public static IConfiguration Configuration { get; set; } = null!;

        /// <summary>
        /// First level topic that all nodes should use.
        /// </summary>
        public const string TopicPrefix = "node";

        /// <summary>
        /// The ClientId value from the configuration.
        /// </summary>
        public static string ClientId => clientId ??= Get<string>("clientId");

        /// <summary>
        /// The Token value from the configuration.
        /// </summary>
        public static string Token => token ??= Get<string>("token");

        /// <summary>
        /// The Server url value from the configuration.
        /// </summary>
        public static string Server => server ??= Get<string>("server").TrimEnd('/');

        /// <summary>
        /// The Certificate path value from the configuration.
        /// </summary>
        public static string Certificate => certificate ??= Get<string>("certificate");

        /// <summary>
        /// The Type of this node from the configuration.
        /// </summary>
        public static string Type => type ??= GetNodeType();

        /// <summary>
        /// Wheter we are in test mode or not.
        /// </summary>
        public static bool Test => test ??= Exists("test");

        private static string GetNodeType()
        {
            var types = new[] { "irrigation", "measurement" };
            var type = Get<string>("dht:version").ToLowerInvariant();
            if (types.Contains(type) is false)
            {
                throw new ArgumentException(
                    $"Type is not valid. Valid types are: {string.Join(" ", types)}.");
            }
            return type;
        }

        /// <summary>
        /// Power Led settings.
        /// </summary>
        public static class Led
        {
            private static int? power;
            private static int? connection;

            /// <summary>
            /// Get the led on which you indicate the app is running.
            /// </summary>
            public static int Power => power ??= Get<int>("led:power");

            /// <summary>
            /// Get the led on which you indicate there is no connection.
            /// </summary>
            public static int Connection => connection ??= Get<int>("led:connection");
        }

        /// <summary>
        /// Dht settings.
        /// </summary>
        public static class Dht
        {
            private static int? pin;
            private static int? led;
            private static int? version;

            /// <summary>
            /// Get the pin on which DHT is connected.
            /// </summary>
            public static int Pin => pin ??= Get<int>("dht:pin");

            /// <summary>
            /// Get the led that indicates DHT is being used.
            /// </summary>
            public static int Led => led ??= Get<int>("dht:led");

            /// <summary>
            /// Get the DHT version to use.
            /// </summary>
            public static int Version => version ??= GetVersion();

            private static int GetVersion()
            {
                var versions = new[] { 11, 12, 21, 22 };
                var version = Get<int>("dht:version");
                if (versions.Contains(version) is false)
                {
                    throw new ArgumentException(
                        $"Dht Version is not valid. Valid versions are: {string.Join(" ", versions)}.");
                }
                return version;
            }
        }

        /// <summary>
        /// Soil sensor settings.
        /// </summary>
        public static class Soil
        {
            private static int? pin;
            private static int? led;

            /// <summary>
            /// Get the pin on which SOIL sensor is connected.
            /// </summary>
            public static int Pin => pin ??= Get<int>("soil:pin");

            /// <summary>
            /// Get the led that indicates SOIL sensor is being used.
            /// </summary>
            public static int Led => led ??= Get<int>("soil:led");
        }

        /// <summary>
        /// Pump settings.
        /// </summary>
        public static class Pump
        {
            private static int? pin;
            private static int? led;
            /// <summary>
            /// Get the pin on which the PUMPT is connected.
            /// </summary>
            public static int Pin => pin ??= Get<int>("Pump:pin");

            /// <summary>
            /// Get the led that indicates the PUMPT is being used.
            /// </summary>
            public static int Led => led ??= Get<int>("Pump:led");
        }

        #region Methods

        /// <summary>
        /// Get a requried value from the configuration.<br/>
        /// If the value doesn't exist an <see cref="ArgumentNullException"/> is raised.<br/>
        /// If it exists but is a default value aka starts with '&lt;' an <see cref="ArgumentException"/> is raised.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="key">The value's key.</param>
        /// <returns>The value for this key or throws an exception.</returns>
        /// <exception cref="ArgumentNullException">When the value doesn't exist.</exception>
        /// <exception cref="ArgumentException">When the value exists but starts with &lt; aka it's a default value.</exception>
        public static T Get<T>(string key)
        {
            var value = Configuration[key];

            return value switch
            {
                null or { Length: 0 } => throw new ArgumentNullException(
                    nameof(key), $"Could not find value for key '{key}'"),

                var val when val.StartsWith("<") => throw new ArgumentException(
                    $"No value has been provided for '{key}'", nameof(key)),

                _ => Configuration.GetValue<T>(key) ?? throw new ArgumentException(
                    $"Invalid value '{value}' for key '{key}'", nameof(key))
            };
        }

        /// <summary>
        /// See if a key exists or not.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <returns><see langword="true"/> if it exists, <see langword="false"/> otherwise.</returns>
        public static bool Exists(string key)
        {
            return Configuration[key] is { };
        }

        #endregion
    }
}