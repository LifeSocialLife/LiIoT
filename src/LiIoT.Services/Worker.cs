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
    using System.Threading;
    using System.Threading.Tasks;
    using LiIoT.Services;
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
        // private readonly NiceToHaveCoreService _niceToHaveCoreService;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="hostappLifetime">IHostApplicationLifetime.</param>
        /// <param name="configuration">IConfiguration.</param>
        /// <param name="rundataService">RunDataService.</param>
        /// <param name="configFileService">ConfigFileService.</param>
        public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostappLifetime, IConfiguration configuration, RunDataService rundataService, ConfigFileService configFileService)
        {
            this._logger = logger;
            this._hostApplicationLifetime = hostappLifetime;
            this._configuration = configuration;
            this._rundata = rundataService;
            this._configfile = configFileService;
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

                if (!this.RunUpstartSchedulerTenToFortyNine())
                {
                    this._rundata.StartUpRunningPart = StartUpRunningPartEnum.Error;
                    return false;
                }
            }

            await Task.Delay(1000);

            // this._rundata.StartUpRunningStage = 1000;
            return true;
        }

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

        /// <summary>
        /// Upstart part. part 10 to 49.
        /// </summary>
        /// <returns>false if error exist.</returns>
        private bool RunUpstartSchedulerTenToFortyNine()
        {
            // Shod we run stage 10 - Configuration file locating
            if (this._rundata.StartUpRunningStage == 10)
            {
                this._configfile.LocateConfigurationFile(true);

                if (string.IsNullOrEmpty(this._rundata.Folders.ConfigFile))
                {
                    return false;
                }

                if (this._rundata.Folders.ConfigFile.ToLower() == "nodata")
                {
                    return false;
                }

                this._rundata.StartUpRunningStage = 11;

                var a1 = this._rundata.Folders;
                this.zzDebug = "sdfdsf";
            }

            // 11 - Read configuration file.
            if (this._rundata.StartUpRunningStage == 11)
            {
                this.zzDebug = "sdfdsf";
            }

            return true;
        }

        private bool GetRundataFolderData()
        {
            this._rundata.Folders.PathRuntimes = DumpData.PathRuntimes; //   this._configuration.GetValue<string>(WebHostDefaults.ContentRootKey);

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
    }
}