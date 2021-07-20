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
    using System.Text;
    using LiIoT.Services.Db;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Device service. Handling devices in software.
    /// </summary>
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
    }
}
