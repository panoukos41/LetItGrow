﻿using System.Threading.Tasks;

namespace LetItGrow.Services
{
    /// <summary>
    /// A distributed cache service.
    /// </summary>
    public interface IDistributedCache
    {
        /// <summary>
        /// Add a new key value pair.
        /// </summary>
        /// <param name="key">The key to use for this value.</param>
        /// <param name="value">The value for this key.</param>
        /// <returns></returns>
        Task KeyAdd(string key, string value);

        /// <summary>
        /// Get the value of a key.
        /// </summary>
        /// <param name="key">The key to get.</param>
        /// <returns>The value of the key or null if it doesn't exist.</returns>
        Task<string?> KeyGet<T>(string key);

        /// <summary>
        /// Delete a key value pair.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        /// <returns></returns>
        Task KeyDelete(string key);

        /// <summary>
        /// Add a new value to a set of values.
        /// </summary>
        /// <param name="set">The set key.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>A task that when completed the operation has completed.</returns>
        Task SetAdd(string set, string value);

        /// <summary>
        /// Get all set values.
        /// </summary>
        /// <param name="set">The set key.</param>
        /// <returns>A task that when completed the operation has completed and
        /// it contains all the set values.</returns>
        Task<string[]> SetMembers(string set);

        /// <summary>
        /// Delete a value from a set.
        /// </summary>
        /// <param name="set">The set key.</param>
        /// <param name="value">The value to delete.</param>
        /// <returns>A task that when completed the operation has completed.</returns>
        Task SetDelete(string set, string value);
    }
}