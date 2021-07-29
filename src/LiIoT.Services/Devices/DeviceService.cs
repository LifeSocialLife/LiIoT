// <summary>
// Device Service.
// </summary>
// <copyright file="DeviceService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using LiIoT.Services.Db;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Device service. Handling devices in software.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Reviewed.")]
    public class DeviceService
    {
        private readonly System.Collections.Concurrent.BlockingCollection<DeviceItemUpdateModel> deviceItemDataUpdate = new();

#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<DeviceService> _logger;
        private readonly RunDataService _rundata;
        private readonly ConfigFileService _configfile;
        private readonly LiteDbService _liteDb;
        private readonly LiteDbServicesDevices _liteDbDevices;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceService"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="rundataService">RunDataService.</param>
        /// <param name="configFileService">ConfigFileService.</param>
        /// <param name="liteDbService">LiteDbService.</param>
        /// <param name="liteDbServicesDevices">LiteDbServicesDevices.</param>
        public DeviceService(ILogger<DeviceService> logger, RunDataService rundataService, ConfigFileService configFileService, LiteDbService liteDbService, LiteDbServicesDevices liteDbServicesDevices)
        {
            this._logger = logger;
            this._rundata = rundataService;
            this._configfile = configFileService;
            this._liteDb = liteDbService;
            this._liteDbDevices = liteDbServicesDevices;
            this.zzDebug = "DeviceService";
        }

        /// <summary>
        /// Sets insert new data (status) into relay or sensor data.
        /// </summary>
        public DeviceItemUpdateModel DeviceItemDataAdd
        {
            set
            {
                this.deviceItemDataUpdate.Add(value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Try to identify the device ??.
        /// </summary>
        /// <param name="ip">ip to device.</param>
        /// <returns>Tuple (bool, DeviceIdentifyModel).</returns>
        public async Task<Tuple<bool, DeviceIdentifyModel>> IdentifyDevice(string ip)
        {
            DeviceIdentifyModel deviceinfo = new() { Manufacturer = DeviceManufacturerEnum.Unknown };

            string source = string.Empty;
            bool connectionWorking = false;

            #region Connect using HTTPS

            string url = $"https://{ip}";
            var tmpPageRead = await LiTools.Helpers.IO.Webpage.ReturnAsString(url);

            if (tmpPageRead.IsWorking)
            {
                source = tmpPageRead.Source;
                connectionWorking = tmpPageRead.IsWorking;
            }

            this.zzDebug = "sdfdsf";

            if (string.IsNullOrEmpty(source))
            {
                connectionWorking = false;
            }

            #endregion

            #region Try connect using HTTP if HTTPS dident werk.

            if (!connectionWorking)
            {
                // Connect using http only.
                url = $"http://{ip}";
                tmpPageRead = await LiTools.Helpers.IO.Webpage.ReturnAsString(url);

                if (tmpPageRead.IsWorking)
                {
                    source = tmpPageRead.Source;
                    connectionWorking = tmpPageRead.IsWorking;
                }

                this.zzDebug = "sfsdf";
            }

            #endregion

            #region Did we get any data using https or http from device ip. if not. return false.

            if (string.IsNullOrEmpty(source))
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                return new Tuple<bool, DeviceIdentifyModel>(false, deviceinfo);
            }

            #endregion

            // Get title from page.
            string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;

            if (string.IsNullOrEmpty(title))
            {
                // no title exist. retun false.
                return new Tuple<bool, DeviceIdentifyModel>(false, deviceinfo);
            }

            if (title.ToLower().Trim().StartsWith("shelly"))
            {
                // This is a shelly device. Try get basic data from uri /shelly
                deviceinfo.Manufacturer = DeviceManufacturerEnum.Shelly;

                // var tmpIdent = new LiIoT.Manufacturer.Shelly.Helpers.
                var dd = new LiIoT.Manufacturer.Shelly.Helpers.IdentifyHelper(ip);


                this.zzDebug = "fdsfd";

            }
            this.zzDebug = "efsdf";

            

            this.zzDebug = "sdfdsf";

            

            /*
             * <head><title>Shelly Switch</title><meta charset=UTF-8>

"Shelly Switch"


172.16.100.200
             * */
            // Console.WriteLine(content);
            this.zzDebug = "sdfdsf";

            return new Tuple<bool, DeviceIdentifyModel>(true, deviceinfo);
        }
    }
}
