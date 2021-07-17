// <summary>
// LiteDb Service
// </summary>
// <copyright file="LiteDbService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Db
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading;
    using LiteDB;
    using LiteDB.Async;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Db lite service.
    /// </summary>
    public class LiteDbService
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        // public readonly object _dblock = new object();

        /// <summary>
        /// Lock access to this db.
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed.")]
        public Semaphore _lock = new(1, 1);

        private readonly ILogger<LiteDbService> _logger;
        private readonly RunDataService _rundata;

        private LiteDatabaseAsync? _db;
        private bool _dbInitIsDone;

#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbService"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="runDataService">RunDataService.</param>
        public LiteDbService(ILogger<LiteDbService> logger, RunDataService runDataService)
        {
            this._logger = logger;
            this._rundata = runDataService;
            this._dbInitIsDone = false;
            this.zzDebug = "sdfdsf";
        }

        /// <summary>
        /// Gets get litedb database connection.
        /// </summary>
        public LiteDatabaseAsync Db
        {
            get
            {
                if (!this._dbInitIsDone)
                {
                    this.DbInit();
                }

#pragma warning disable CS8603 // Possible null reference return.
                return this._db;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Do init of the database connection.
        /// </summary>
        /// <returns>true if the db init was done.</returns>
        public bool DbInit()
        {
            // Build Path to db directory and file file.
            var tmpPath = new System.IO.FileInfo(System.IO.Path.Combine(this._rundata.Folders.PathData, "db"));
            var tmpfile = new System.IO.FileInfo(System.IO.Path.Combine(tmpPath.FullName, SoftwareRulesAndStaticData.DbFilename));

            this.zzDebug = "dsdsf";

            // Check if path exist. if the path dont exist. create path.
            if (!LiTools.Helpers.IO.Directory.Exist(tmpPath.FullName))
            {
                // Dont exist. Create directory.
                if (!LiTools.Helpers.IO.Directory.DirectoryCreate(tmpPath.FullName))
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }
            }

            string conString = $"Filename={@tmpfile.FullName};Connection=direct;Password=FEF3342JKDi323;";

            try
            {
                // this._db = new LiteDatabase(conString);
                this._db = new LiteDatabaseAsync(conString);
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                return false;
            }

            this.zzDebug = "sdfdsf";
            this._dbInitIsDone = true;

            return true;
        }
    }
}
