using LetItGrow.Services;
using System;

namespace LetItGrow.CoreHost.Services
{
    /// <summary>
    /// Implementation of <see cref="IClockService"/>
    /// </summary>
    public class ClockService : IClockService
    {
        /// <inheritdoc/>
        public DateTimeOffset GetNow()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}