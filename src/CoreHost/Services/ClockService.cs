using LetItGrow.Services;
using NodaTime;

namespace LetItGrow.CoreHost.Services
{
    /// <summary>
    /// todo: summary
    /// </summary>
    public class ClockService : IClockService
    {
        public Instant GetCurrentInstant()
        {
            return SystemClock.Instance.GetCurrentInstant();
        }
    }
}