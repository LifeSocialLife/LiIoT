// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="Worker.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace StarterNoGui
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
    public class Worker : BackgroundService
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<Worker> _logger;
        private readonly RunDataService _rundata;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="Worker"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="hostappLifetime">IHostApplicationLifetime.</param>
        /// <param name="configuration">IConfiguration.</param>
        /// <param name="rundataService">RunDataService.</param>
        public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostappLifetime, IConfiguration configuration, RunDataService rundataService)
        {
            this._logger = logger;
            this._hostApplicationLifetime = hostappLifetime;
            this._configuration = configuration;
            this._rundata = rundataService;
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

            var d2 = this._configuration.GetValue<string>(WebHostDefaults.ContentRootKey);

            this.zzDebug = "sdfdf";

            while (!stoppingToken.IsCancellationRequested)
            {
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
