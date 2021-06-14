// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Device;
using System.Device.Gpio;
using System.Diagnostics;
using System.Threading;
using UnitsNet;

namespace Iot.Device.DHTxx
{
    /// <summary>
    /// Temperature and Humidity Sensor DHTxx
    /// </summary>
    public abstract class DhtBase
    {
        /// <summary>
        /// Read buffer
        /// </summary>
        protected byte[] _readBuff = new byte[5];

        /// <summary>
        /// GPIO pin
        /// </summary>
        protected readonly int _pin;

        /// <summary>
        /// <see cref="GpioController"/> related with the <see cref="_pin"/>.
        /// </summary>
        protected GpioController _controller;

        // wait about 1 ms
        private readonly uint _loopCount = 10000;

        private readonly Stopwatch _stopwatch = new();
        private int _lastMeasurement = 0;

        /// <summary>
        /// How last read went, <c>true</c> for success, <c>false</c> for failure
        /// </summary>
        public bool IsLastReadSuccessful { get; internal set; }

        /// <summary>
        /// Create a DHT sensor
        /// </summary>
        /// <param name="pin">The pin number (GPIO number)</param>
        /// <param name="controller">The gpio controller to use.</param>
        protected DhtBase(int pin, GpioController controller)
        {
            _controller = controller;
            _pin = pin;

            // delay 1s to make sure DHT stable
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Try to read sensor measurements.
        /// </summary>
        /// <param name="measurement">The parameter to store the data.</param>
        /// <returns>True when dataread suscesfull false otherwise.</returns>
        public bool TryMeasure(out (Temperature Temperature, RelativeHumidity Humidity) measurement)
        {
            measurement = (new Temperature(), new RelativeHumidity());

            // The time of two measurements should be more than 1s.
            if ((Environment.TickCount - _lastMeasurement < 1000)
                || IsLastReadSuccessful is false)
            {
                return false;
            }

            ReadData();

            measurement = (GetTemperature(_readBuff), GetHumidity(_readBuff));
            return true;
        }

        /// <summary>
        /// Read through One-Wire
        /// </summary>
        internal virtual void ReadData()
        {
            if (_controller is null)
            {
                throw new Exception("GPIO controller is not configured.");
            }

            byte readVal = 0;
            uint count;
            var pinMode = _controller.IsPinModeSupported(_pin, PinMode.InputPullUp) ? PinMode.InputPullUp : PinMode.Input;

            // keep data line HIGH
            _controller.SetPinMode(_pin, PinMode.Output);
            _controller.Write(_pin, PinValue.High);
            DelayHelper.DelayMilliseconds(20, true);

            // send trigger signal
            _controller.Write(_pin, PinValue.Low);
            // wait at least 18 milliseconds
            // here wait for 18 milliseconds will cause sensor initialization to fail
            DelayHelper.DelayMilliseconds(20, true);

            // pull up data line
            _controller.Write(_pin, PinValue.High);
            // wait 20 - 40 microseconds
            DelayHelper.DelayMicroseconds(30, true);

            _controller.SetPinMode(_pin, pinMode);

            // DHT corresponding signal - LOW - about 80 microseconds
            count = _loopCount;
            while (_controller.Read(_pin) == PinValue.Low)
            {
                if (count-- == 0)
                {
                    IsLastReadSuccessful = false;
                    return;
                }
            }

            // HIGH - about 80 microseconds
            count = _loopCount;
            while (_controller.Read(_pin) == PinValue.High)
            {
                if (count-- == 0)
                {
                    IsLastReadSuccessful = false;
                    return;
                }
            }

            // the read data contains 40 bits
            for (int i = 0; i < 40; i++)
            {
                // beginning signal per bit, about 50 microseconds
                count = _loopCount;
                while (_controller.Read(_pin) == PinValue.Low)
                {
                    if (count-- == 0)
                    {
                        IsLastReadSuccessful = false;
                        return;
                    }
                }

                // 26 - 28 microseconds represent 0
                // 70 microseconds represent 1
                _stopwatch.Restart();
                count = _loopCount;
                while (_controller.Read(_pin) == PinValue.High)
                {
                    if (count-- == 0)
                    {
                        IsLastReadSuccessful = false;
                        return;
                    }
                }

                _stopwatch.Stop();

                // bit to byte
                // less than 40 microseconds can be considered as 0, not necessarily less than 28 microseconds
                // here take 30 microseconds
                readVal <<= 1;
                if (!(_stopwatch.ElapsedTicks * 1000000F / Stopwatch.Frequency <= 30))
                {
                    readVal |= 1;
                }

                if (((i + 1) % 8) == 0)
                {
                    _readBuff[i / 8] = readVal;
                }
            }

            _lastMeasurement = Environment.TickCount;

            if ((_readBuff[4] == ((_readBuff[0] + _readBuff[1] + _readBuff[2] + _readBuff[3]) & 0xFF)))
            {
                IsLastReadSuccessful = (_readBuff[0] != 0) || (_readBuff[2] != 0);
            }
            else
            {
                IsLastReadSuccessful = false;
            }
        }

        /// <summary>
        /// Converting data to humidity
        /// </summary>
        /// <param name="readBuff">Data</param>
        /// <returns>Humidity</returns>
        internal abstract RelativeHumidity GetHumidity(byte[] readBuff);

        /// <summary>
        /// Converting data to Temperature
        /// </summary>
        /// <param name="readBuff">Data</param>
        /// <returns>Temperature</returns>
        internal abstract Temperature GetTemperature(byte[] readBuff);
    }
}