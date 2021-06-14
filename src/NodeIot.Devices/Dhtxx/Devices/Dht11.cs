// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Device.Gpio;
using UnitsNet;

namespace Iot.Device.DHTxx
{
    /// <summary>
    /// Temperature and Humidity Sensor DHT11
    /// </summary>
    public class Dht11 : DhtBase
    {
        /// <inheritdoc/>
        public Dht11(int pin, GpioController controller)
            : base(pin, controller)
        {
        }

        internal override RelativeHumidity GetHumidity(byte[] readBuff) => RelativeHumidity.FromPercent(readBuff[0] + readBuff[1] * 0.1);

        internal override Temperature GetTemperature(byte[] readBuff)
        {
            var temp = readBuff[2] + readBuff[3] * 0.1;
            return Temperature.FromDegreesCelsius(temp);
        }
    }
}