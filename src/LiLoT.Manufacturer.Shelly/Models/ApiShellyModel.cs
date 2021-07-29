// <summary>
// Device Service.
// </summary>
// <copyright file="ApiShellyModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Manufacturer.Shelly.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Newtonsoft.Json;

    /// <summary>
    /// shelly.
    /// Provides basic information about the device. It does not require HTTP authentication, even if authentication is enabled globally.
    /// This endpoint can be used in ///conjunction with mDNS for device discovery and identification. It accepts no parameters.
    /// </summary>
    public class ApiShellyModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiShellyModel"/> class.
        /// </summary>
        public ApiShellyModel()
        {
            this.Type = string.Empty;
            this.Mac = string.Empty;
            this.Auth = string.Empty;
            this.Firmware = string.Empty;
            this.Longid = 0;
        }

        /// <summary>
        /// Gets or sets shelly model identifier.
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets mAC address of the device.
        /// </summary>
        [JsonProperty("mac", NullValueHandling = NullValueHandling.Ignore)]
        public string Mac { get; set; }

        /// <summary>
        /// Gets or sets whether HTTP requests require authentication.
        /// </summary>
        [JsonProperty("auth", NullValueHandling = NullValueHandling.Ignore)]
        public string Auth { get; set; }

        /// <summary>
        /// Gets or sets current firmware version.
        /// </summary>
        [JsonProperty("fw", NullValueHandling = NullValueHandling.Ignore)]
        public string Firmware { get; set; }

        /// <summary>
        /// Gets or sets 1 if the device identifies itself with its full MAC address; 0 if only the last 3 bytes are used.
        /// </summary>
        [JsonProperty("longid", NullValueHandling = NullValueHandling.Ignore)]
        public ushort Longid { get; set; }
    }
}
