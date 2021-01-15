using System;

namespace NodaTime
{
    public static class NodaTimeExtensions
    {
        public static DateTime ToLocalTime(this Instant instant)
        {
            return instant.ToDateTimeUtc().ToLocalTime();
        }
    }
}