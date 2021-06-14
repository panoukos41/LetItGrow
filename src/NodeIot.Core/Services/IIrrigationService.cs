namespace LetItGrow.NodeIot.Services
{
    public interface IIrrigationService
    {
        /// <summary>
        /// Set the state of the irrigation component.
        /// True to start irrigating false to stop.
        /// </summary>
        /// <param name="state">The state of the irrigation. True to start False to stop.</param>
        void Set(bool state);
    }
}