using System.Device.Gpio;

namespace LetItGrow.NodeIot.Common
{
    /// <summary>
    /// Group a pin and a controller to manage mode and values using properties etc.
    /// </summary>
    public sealed class Pin
    {
        private readonly GpioController _gpio;
        private readonly int _pin;

        /// <summary>
        /// Initialize a new <see cref="Pin"/> utility class.
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="gpio"></param>
        public Pin(int pin, GpioController gpio)
        {
            _pin = pin;
            _gpio = gpio;
        }

        /// <summary>
        /// Get or set the mode of the pin.
        /// </summary>
        /// <remarks>Under the hood the controller is used for either operation.</remarks>
        public PinMode Mode
        {
            get => _gpio.GetPinMode(_pin);
            set => _gpio.SetPinMode(_pin, value);
        }

        /// <summary>
        /// Get or set the value of the pin.
        /// </summary>
        /// <remarks>Under the hood the controller is used for either operation.</remarks>
        public PinValue Value
        {
            get => _gpio.Read(_pin);
            set => _gpio.Write(_pin, value);
        }

        /// <summary>
        /// Get if the pin is open.
        /// </summary>
        public bool IsPinOpen => _gpio.IsPinOpen(_pin);

        /// <summary>
        /// Opens the pin in order for it to be ready to use.
        /// </summary>
        public void Open() => _gpio.OpenPin(_pin);

        /// <summary>
        /// Opens the pin and sets it to a specific mode.
        /// </summary>
        public void Open(PinMode mode) => _gpio.OpenPin(_pin, mode);

        /// <summary>
        /// Closes the open pin.
        /// </summary>
        public void Close() => _gpio.ClosePin(_pin);

        /// <summary>
        /// Checks if the pin supports a specific mode.
        /// </summary>
        /// <param name="mode">The mode to check.</param>
        /// <returns>True if the pin supports the mode.</returns>
        public bool IsPinModeSupported(PinMode mode) => _gpio.IsPinModeSupported(_pin, mode);

        /// <summary>
        /// Returns the pin number.
        /// </summary>
        /// <returns>The pin number.</returns>
        public override string ToString() => _pin.ToString();

        public static implicit operator int(Pin pin) => pin._pin;

        public static implicit operator PinMode(Pin pin) => pin.Mode;

        public static implicit operator PinValue(Pin pin) => pin.Value;

        public static implicit operator GpioController(Pin pin) => pin._gpio;
    }
}