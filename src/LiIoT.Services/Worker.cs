// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="Worker.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Resources;
    using System.Threading;
    using System.Threading.Tasks;
    using LiIoT.Services;
    using LiIoT.Services.Db;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Worker class. Main background work.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Reviewed.")]
    public class Worker : BackgroundService
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<Worker> _logger;
        private readonly RunDataService _rundata;
        private readonly ConfigFileService _configfile;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly LiteDbService _liteDb;
        private readonly LiteDbServicesDevices _liteDbDevices;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="hostappLifetime">IHostApplicationLifetime.</param>
        /// <param name="configuration">IConfiguration.</param>
        /// <param name="rundataService">RunDataService.</param>
        /// <param name="configFileService">ConfigFileService.</param>
        /// <param name="liteDbService">LiteDbService.</param>
        /// <param name="liteDbServicesDevices">LiteDbServicesDevices.</param>
        public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostappLifetime, IConfiguration configuration, RunDataService rundataService, ConfigFileService configFileService, LiteDbService liteDbService, LiteDbServicesDevices liteDbServicesDevices)
        {
            this._logger = logger;
            this._hostApplicationLifetime = hostappLifetime;
            this._configuration = configuration;
            this._rundata = rundataService;
            this._configfile = configFileService;
            this._liteDb = liteDbService;
            this._liteDbDevices = liteDbServicesDevices;
            this.zzDebug = "Worker";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this._hostApplicationLifetime.ApplicationStarted.Register(this.OnStarted, true);
            this._hostApplicationLifetime.ApplicationStopping.Register(this.OnStopping, true);
            this._hostApplicationLifetime.ApplicationStopped.Register(this.OnStopped, true);

            this.zzDebug = "sdfdf";

            this._rundata.StartUpRunningStage = 1;

            while (!stoppingToken.IsCancellationRequested)
            {
                // Do we have a error in software that is stopping it from starting?.
                if (this._rundata.StartUpRunningPart == StartUpRunningPartEnum.Error)
                {
                    await Task.Delay(5000, stoppingToken);
                    continue;
                }

                // Run Stage is below 1000. software is in startupScheduler.
                if (this._rundata.StartUpRunningStage < 1000)
                {
                    if (await this.RunUpstartScheduler())
                    {
                    }

                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                var a1 = this._rundata.Folders;
                var a2 = this._rundata.Hardware;
                this.zzDebug = "SDfdsf";

                this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    await Task.Delay(1000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        #region Upstart Scheduler Items

        private async Task<bool> RunUpstartScheduler()
        {
            if (this._rundata.StartUpRunningStage < 10)
            {
                this._rundata.StartUpRunningPart = StartUpRunningPartEnum.Init;

                if (!this.RunUpstartSchedulerOneToNine())
                {
                    this._rundata.StartUpRunningPart = StartUpRunningPartEnum.Error;
                    return false;
                }
            }

            if (this._rundata.StartUpRunningStage < 50)
            {
                this._rundata.StartUpRunningPart = StartUpRunningPartEnum.ReadingConfiguration;

                if (!await this.RunUpstartSchedulerTenToFortyNine())
                {
                    this._rundata.StartUpRunningPart = StartUpRunningPartEnum.Error;
                    return false;
                }
            }

            await Task.Delay(1000);

            // this._rundata.StartUpRunningStage = 1000;
            return true;
        }

        #region Upstart 1 to 9

        /// <summary>
        /// Upstart part. part 1 to 9.
        /// </summary>
        /// <returns>false if error exist.</returns>
        private bool RunUpstartSchedulerOneToNine()
        {
            // Shod we run stage 1 - Get Folder inforamtion.
            if (this._rundata.StartUpRunningStage == 1)
            {
                if (this.GetRundataFolderData())
                {
                    Task.Delay(1000);
                    this._rundata.StartUpRunningStage = 2;
                }
                else
                {
                    this._rundata.StartUpRunningPart = StartUpRunningPartEnum.Error;
                    return false;
                }
            }

            // Shod we run stage 2 - Get hardware inforamtion.
            if (this._rundata.StartUpRunningStage == 2)
            {
                if (this._rundata.SetHardwareModel())
                {
                    Task.Delay(1000);
                    this._rundata.StartUpRunningStage = 3;
                }
                else
                {
                    this._rundata.StartUpRunningPart = StartUpRunningPartEnum.Error;
                    return false;
                }
            }

            this._rundata.StartUpRunningStage = 10;
            return true;
        }

        #endregion

        /// <summary>
        /// Upstart part. part 10 to 49.
        /// </summary>
        /// <returns>false if error exist.</returns>
        private async Task<bool> RunUpstartSchedulerTenToFortyNine()
        {
            // Shod we run stage 10 - Configuration file locating
            if (this._rundata.StartUpRunningStage == 10)
            {
                var tmpPath = this._configfile.LocateConfigurationFile(true);

                if (string.IsNullOrEmpty(tmpPath))
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }
                else if (tmpPath == "nodata")
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }

                this._rundata.Folders.ConfigFile = tmpPath;

                this._rundata.StartUpRunningStage = 11;

                this.zzDebug = "sdfdsf";
            }

            // 11 - Read configuration file.
            if (this._rundata.StartUpRunningStage == 11)
            {
                if (!this._configfile.ConfigurationFileRead())
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }

                this._rundata.StartUpRunningStage = 12;
                var a1 = this._rundata.Folders;
                this.zzDebug = "sdfdsf";
            }

            // 12 - Check pathdata folder from config file.
            if (this._rundata.StartUpRunningStage == 12)
            {
                // Check if we have datapath from configfile.
                if (string.IsNullOrEmpty(this._configfile.ConfigFile.PathData))
                {
                    // Path data folder dont exist in configuration file.
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }

                // Move datapath from configfile into rundata.
                this._rundata.Folders.PathData = this._configfile.ConfigFile.PathData;

                // Check if pathdata folder exist.
                if (!LiTools.Helpers.IO.Directory.Exist(this._rundata.Folders.PathData))
                {
                    // DataPath dont exist....
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }

                this._rundata.StartUpRunningStage = 13;
                var a1 = this._rundata.Folders;
                this.zzDebug = "sdfdsf";

                // if (System.Diagnostics.Debugger.IsAttached)
                // {
                //    System.Diagnostics.Debugger.Break();
                // }
            }

            // 13 - Check litedb storage.
            if (this._rundata.StartUpRunningStage == 13)
            {
                this.zzDebug = "Sdfdsf";
                this._liteDb.DbInit();

                this.zzDebug = "sdfd";

                var aa = await this._liteDbDevices.GetAll();

                // Task.WaitAll(aa);
                this.zzDebug = "sdfdf";
            }

            this._rundata.StartUpRunningStage = 1000;
            return true;
        }

        private bool GetRundataFolderData()
        {
            if (!string.IsNullOrEmpty(SoftwareRulesAndStaticData.PathRuntimes))
            {
                this._rundata.Folders.PathRuntimes = SoftwareRulesAndStaticData.PathRuntimes;
            }

#pragma warning disable CS8601 // Possible null reference assignment.
            if (System.Diagnostics.Process.GetCurrentProcess()?.MainModule?.FileName != null)
            {
                this._rundata.Folders.PathExecuteFile = System.Diagnostics.Process.GetCurrentProcess()?.MainModule?.FileName;
            }

            if (this._rundata.Folders.PathExecuteFile != string.Empty)
            {
                this._rundata.Folders.PathExecute = System.IO.Path.GetDirectoryName(this._rundata.Folders.PathExecuteFile);
            }
#pragma warning restore CS8601 // Possible null reference assignment.

            return true;
        }

        #endregion

        #region OnStarted, OnStopping, OnStopped

        private void OnStarted()
        {
            this._logger.LogInformation("OnStarted has been called.");

            this.zzDebug = "sdfdf";

            // Perform post-startup activities here
        }

        private void OnStopping()
        {
            this._logger.LogInformation("NodeWorker | OnStopping | Stop all backgrounds work.");
            this._logger.LogInformation("NodeWorker | OnStopping | Stop all backgrounds work. | Done");

            // _logger.LogInformation("OnStopping has been called.");
            this.zzDebug = "sdfdf";

            // Perform on-stopping activities here
        }

        private void OnStopped()
        {
            // Perform post-stopped activities here
            this._logger.LogInformation("OnStopped has been called.");

            this.zzDebug = "sdfdf";
        }

        #endregion
    }
}
