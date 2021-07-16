// <summary>
// LiteDb Service - Devices
// </summary>
// <copyright file="LiteDbServicesDevices.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

/*
 * https://github.com/mlockett42/litedb-async
 * */

namespace LiIoT.Services.Db
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading.Tasks;
    using LiteDB;
    using LiteDB.Async;
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

        private LiteCollectionAsync<DevicesModel> col;

        private bool InitIsDone { get; set; }

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
            this.InitIsDone = false;
        }

        [SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        

        /// <summary>
        /// Get all devices.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Tuple<bool, List<DevicesModel>>> GetAll()
        {
            this.Init();
            var aa = new List<DevicesModel>();
            var error = false;
            this._db._lock.WaitOne(-1);

            try
            {
                aa = await this.col.Query().ToListAsync();
            }
            catch (Exception e)
            {
                error = true;
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }
            }
            finally
            {
                this._db._lock.Release();
            }

            // var h3 = await this.col.Query().ToListAsync();
            this.zzDebug = "dsfdsf";

            if (error)
            {
                return new Tuple<bool, List<DevicesModel>>(false, new List<DevicesModel>());
            }

            return new Tuple<bool, List<DevicesModel>>(true, aa);
        }

        private void Init()
        {
            if (!this.InitIsDone)
            {
                this.col = this._db.Db.GetCollection<DevicesModel>("devices");
                this.InitIsDone = true;
            }
        }

        private async Task Testing()
        {
            var db = this._db.Db;

            var col = db.GetCollection<DevicesModel>("devices");

            var h2 = await col.Query().ToListAsync();

            this.zzDebug = "dsfdsf";

            var a1 = new DevicesModel()
            {
                Text = "hej2",
            };

            this.zzDebug = "sdfdsf";

            await col.EnsureIndexAsync(x => x.Id, true);
            this.zzDebug = "sdfdsf";

            await col.InsertAsync(a1);
            this.zzDebug = "sdfdsf";
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
