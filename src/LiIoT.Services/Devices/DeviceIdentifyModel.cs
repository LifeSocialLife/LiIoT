// <summary>
// DeviceIdentifyModel.
// </summary>
// <copyright file="DeviceIdentifyModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Device manufacturers.
    /// </summary>
    public enum DeviceManufacturerEnum
    {
        /// <summary>Unknown manufacture.</summary>
        Unknown = 0,

        /// <summary>Shelly device.</summary>
        Shelly = 1,
    }

    /// <summary>Device Identify Model.</summary>
    public class DeviceIdentifyModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceIdentifyModel"/> class.
        /// </summary>
        public DeviceIdentifyModel()
        {
            this.Manufacturer = DeviceManufacturerEnum.Unknown;
        }

        /// <summary>Gets or sets device Manufacturer.</summary>
        public DeviceManufacturerEnum Manufacturer { get; set; }
    }
}
