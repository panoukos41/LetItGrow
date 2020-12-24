namespace LetItGrow.Web.Data.Irrigations.Models
{
    /// <summary>
    /// The type of an <see cref="IrrigationModel"/> object.
    /// </summary>
    public enum IrrigationType : byte
    {
        Invalid = 0,
        Start = 1,
        Stop = 2
    }
}