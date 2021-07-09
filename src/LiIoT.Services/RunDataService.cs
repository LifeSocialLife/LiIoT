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
    using Microsoft.Extensions.Logging;

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
        }

        /// <summary>
        /// Gets or sets folder information for this software.
        /// </summary>
        public RundataServiceFoldersModel Folders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }
    }
}
