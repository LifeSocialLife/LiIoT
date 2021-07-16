// <summary>
// LiteDb Service - Devices
// </summary>
// <copyright file="LiteDbServicesDevices.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Db
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using LiteDB;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Db lite service. Devices.
    /// </summary>
    public class LiteDbServicesDevices
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<LiteDbServicesDevices> _logger;
        private readonly RunDataService _rundata;
        private readonly LiteDbService _db;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbServicesDevices"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="runDataService">RunDataService.</param>
        /// <param name="liteDbService">LiteDbService.</param>
        public LiteDbServicesDevices(ILogger<LiteDbServicesDevices> logger, RunDataService runDataService, LiteDbService liteDbService)
        {
            this._logger = logger;
            this._rundata = runDataService;
            this._db = liteDbService;
            this.zzDebug = "LiteDbServicesDevices";
        }

        [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Get all devices.
        /// </summary>
        public List<DevicesModel> GetAll()
        {
            var db = this._db.Db;

            var col = db.GetCollection<DevicesModel>("devices");

            this.zzDebug = "dsfdsf";

            var a1 = new DevicesModel()
            {
                Text = "hej2",
            };
            this.zzDebug = "sdfdsf";

            col.EnsureIndex(x => x.Id, true);
            this.zzDebug = "sdfdsf";

            col.Insert(a1);
            this.zzDebug = "sdfdsf";

            return new List<DevicesModel>();

        }
    }

    /// <summary>
    /// Device model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Reviewed.")]
    public class DevicesModel
    {
        public ObjectId Id { get; set; }
        public string Text { get; set; }

    }
}
