using Serilog;

namespace LetItGrow.NodeIot.Services
{
    public sealed class TestIrrigationService : IIrrigationService
    {
        public void Set(bool state)
        {
            Log.Information("Test Irrigation set to '{state}'", state);
        }
    }
}