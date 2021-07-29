// <summary>
// Device enums types.
// </summary>
// <copyright file="DeviceEnums.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Device Enums.
    /// </summary>
    public static class DeviceEnums
    {
        /// <summary>
        /// Device items types enums.
        /// </summary>
        public enum DeviceItemsTypes
        {
            /// <summary>
            /// Relay.
            /// </summary>
            Relay = 0,

            /// <summary>
            /// Sensor.
            /// </summary>
            Sensor = 1,
        }
    }
}
