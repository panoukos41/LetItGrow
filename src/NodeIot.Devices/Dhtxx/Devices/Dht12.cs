// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Device.Gpio;
using UnitsNet;

namespace Iot.Device.DHTxx
{
    /// <summary>
    /// Temperature and Humidity Sensor DHT12
    /// </summary>
    public class Dht12 : DhtBase
    {
        /// <inheritdoc/>
        public Dht12(int pin, GpioController controller)
            : base(pin, controller)
        {
        }

        internal override RelativeHumidity GetHumidity(byte[] readBuff)
        {
            return RelativeHumidity.FromPercent(readBuff[0] + readBuff[1] * 0.1);
        }

        internal override Temperature GetTemperature(byte[] readBuff)
        {
            var temp = readBuff[2] + (readBuff[3] & 0x7F) * 0.1;
            // if MSB = 1 we have negative temperature
            temp = (readBuff[3] & 0x80) == 0 ? temp : -temp;

            return Temperature.FromDegreesCelsius(temp);
        }
    }
}