// <summary>
// Shelly Device Identify Helper.
// </summary>
// <copyright file="IdentifyHelper.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Manufacturer.Shelly.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Shelly Identifier.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Reviewed.")]
    public class IdentifyHelper
    {
        private readonly IdentifyHelperConnectionData connectionData;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifyHelper"/> class.
        /// </summary>
        /// <param name="ip">Shelly device ip.</param>
        /// <param name="port">shelly api port.</param>
        /// <param name="username">Auth username.</param>
        /// <param name="password">Auth password.</param>
        public IdentifyHelper(string ip, ushort port = 80, string username = "", string password = "")
        {
            this.zzDebug = "IdentifyHelper";
            this.IdentifyeringDone = false;
            this.connectionData = new IdentifyHelperConnectionData()
            {
                Ip = ip,
                Port = port,
                Password = password,
                Username = username,
                UseAuthentication = false,
            };

            if ((!string.IsNullOrEmpty(username)) || (!string.IsNullOrEmpty(password)))
            {
                this.connectionData.UseAuthentication = true;
            }

            _ = this.Identify();
        }

        public bool IdentifyeringDone { get; private set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }


        private async Task Identify()
        {
            #region Get api /shelly information

            var tmpPageRead = await LiTools.Helpers.IO.Webpage.ReturnAsString($"{this.connectionData.GetConnectionUrl}shelly");

            this.zzDebug = "sdfdsf";

            #endregion

            this.IdentifyeringDone = true;
        }


    }

    public class IdentifyHelperConnectionData
    {
        public IdentifyHelperConnectionData()
        {
            this.Ip = string.Empty;
            this.Port = 80;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.UseAuthentication = false;
        }

        public string Ip { get; set; }
        public ushort Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UseAuthentication { get; set; }

        public string GetConnectionUrl { get { return $"http://{this.Ip}:{this.Port}/";  } }
        
    }

}
