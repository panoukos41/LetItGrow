using System;

namespace LetItGrow.Services
{
    /// <summary>
    /// A service that returns the current <see cref="DateTimeOffset"/> in utc.
    /// </summary>
    public interface IClockService
    {
        /// <summary>
        /// Returns the time now in utc
        /// </summary>
        /// <returns></returns>
        DateTimeOffset GetNow();
    }
}