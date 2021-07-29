// <summary>
// Shelly Device connection model.
// </summary>
// <copyright file="IdentifyHelperConnectionDataModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Manufacturer.Shelly.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Identify Connection Model.
    /// </summary>
    public class IdentifyHelperConnectionDataModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifyHelperConnectionDataModel"/> class.
        /// </summary>
        public IdentifyHelperConnectionDataModel()
        {
            this.Ip = string.Empty;
            this.Port = 80;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.UseAuthentication = false;
        }

        /// <summary>
        /// Gets or sets ip adress.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets port.
        /// </summary>
        public ushort Port { get; set; }

        /// <summary>
        /// Gets or sets username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is the device using authentication.
        /// </summary>
        public bool UseAuthentication { get; set; }

        /// <summary>
        /// Gets connectionstring to this device.
        /// </summary>
        public string GetConnectionUrl
        {
            get
            {
                return $"http://{this.Ip}:{this.Port}/";
            }
        }
    }
}
