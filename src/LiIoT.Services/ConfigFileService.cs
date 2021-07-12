// <summary>
// Rundata Service.
// </summary>
// <copyright file="ConfigFileService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Configuration Service. All service that is handling configuration file.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements", Justification = "Reviewed.")]
    public class ConfigFileService
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<ConfigFileService> _logger;
        private readonly RunDataService _rundata;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigFileService"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="runDataService">RunDataService.</param>
        public ConfigFileService(ILogger<ConfigFileService> logger, RunDataService runDataService)
        {
            this._logger = logger;
            this._rundata = runDataService;
            this.zzDebug = "ConfigurationService";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Locate where the config file is stored.
        /// </summary>
        /// <param name="saveToRundata">Shod the path be saved to rundata service?.</param>
        /// <returns>Path to the file.</returns>
        public string LocateConfigurationFile(bool saveToRundata = true)
        {
            string pathFolder = "liiot";
            string pathFilename = "liiotdata.conf";

            #region Locate in run folder

            var tmpfile = new FileInfo(Path.Combine(Environment.CurrentDirectory, pathFilename));

            if (tmpfile.Exists)
            {
                this.zzDebug = "dsfdsf";
                return tmpfile.FullName;
            }

            #endregion

            #region Locate in Appdata local folder

            tmpfile = new FileInfo(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), pathFolder, pathFilename));

            if (tmpfile.Exists)
            {
                this.zzDebug = "dsfdsf";
                return tmpfile.FullName;
            }

            #endregion

            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }

            return string.Empty;
        }
    }
}
