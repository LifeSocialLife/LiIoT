// <summary>
// Rundata Service.
// </summary>
// <copyright file="RunDataService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using LiIoT.Models.Rundata;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// software running as enum.
    /// </summary>
    public enum RunningAsEnum
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Master. this is the master node.
        /// </summary>
        Master,

        /// <summary>
        /// Slave. This node is slave.
        /// </summary>
        Slave,
    }

    /// <summary>
    /// Where is the software during startup phase.
    /// </summary>
    public enum StartUpRunningPartEnum
    {
        /// <summary>
        /// Application started.
        /// </summary>
        None = 0,

        /// <summary>
        /// Application Checking node hardware information.
        /// </summary>
        Init = 1,

        /// <summary>
        /// Reading software coniguration file.
        /// </summary>
        ReadingConfiguration = 2,

        /// <summary>
        /// Reading software data files.
        /// </summary>
        ReadingDataFiles = 3,

        /// <summary>
        /// Checking other nodes (master/slaves).
        /// </summary>
        CheckingNodes = 4,

        /// <summary>
        /// Starting background services.
        /// </summary>
        StartingServices = 5,

        /// <summary>
        /// Software loading and checks is done. Shod this node be master or slave?.
        /// </summary>
        Standby = 6,

        /// <summary>
        /// Software is running.
        /// </summary>
        Running = 7,

        /// <summary>
        /// Error during startup.
        /// </summary>
        Error = 8,
    }

    /// <summary>
    /// RunData Service.
    /// </summary>
    public class RunDataService
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<RunDataService> _logger;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="RunDataService"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        public RunDataService(ILogger<RunDataService> logger)
        {
            this._logger = logger;
            this.zzDebug = "RunDataService";
            this.Folders = new RundataServiceFoldersModel();
            this.Hardware = new RundataServiceHardwareModel();
            this.RunningAs = RunningAsEnum.None;
            this.StartUpRunningStage = 0;
            this.StartUpRunningPart = StartUpRunningPartEnum.None;
        }

        /// <summary>
        /// Gets or sets folder information for this software.
        /// </summary>
        public RundataServiceFoldersModel Folders { get; set; }

        /// <summary>
        /// Gets or sets hardware information about the computer this is running on.
        /// </summary>
        public RundataServiceHardwareModel Hardware { get; set; }

        /// <summary>
        /// Gets or sets is this running as master or slave?.
        /// </summary>
        public RunningAsEnum RunningAs { get; set; }

        /// <summary>
        /// Gets or sets what section of the startup process is running now.
        /// </summary>
        public StartUpRunningPartEnum StartUpRunningPart { get; set; }

        /// <summary>
        /// Gets or sets running stage. Where in upstart is this software.
        /// </summary>
        public ushort StartUpRunningStage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Set all data into Hardware model. collect using LiTools harware helper.
        /// </summary>
        /// <returns>True if we have all values. false if value is missing or wrong.</returns>
        public bool SetHardwareModel()
        {
            this.Hardware.FrameworkDescription = LiTools.Helpers.IO.Hardware.GetFrameworkDescription();
            this.Hardware.OSArchitecture = LiTools.Helpers.IO.Hardware.GetOSArchitecture();
            this.Hardware.Os = LiTools.Helpers.IO.Hardware.GetOs();
            this.Hardware.OsDescription = LiTools.Helpers.IO.Hardware.GetOsDescription();
            this.Hardware.ProcessorArchitecture = LiTools.Helpers.IO.Hardware.GetProcessorArchitecture();

            // Check all collected values. Return true if we have values.
            if (string.IsNullOrEmpty(this.Hardware.FrameworkDescription))
            {
                return false;
            }

            if (this.Hardware.OSArchitecture == LiTools.Helpers.IO.Hardware.ArchitectureEnum.None)
            {
                return false;
            }

            if ((this.Hardware.Os == LiTools.Helpers.IO.Hardware.PlatformOsEnum.None) || (this.Hardware.Os == LiTools.Helpers.IO.Hardware.PlatformOsEnum.Unknown))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Hardware.OsDescription))
            {
                return false;
            }

            if (this.Hardware.ProcessorArchitecture == LiTools.Helpers.IO.Hardware.ArchitectureEnum.None)
            {
                return false;
            }

            this.zzDebug = "fsdf";
            return true;
        }
    }
}
