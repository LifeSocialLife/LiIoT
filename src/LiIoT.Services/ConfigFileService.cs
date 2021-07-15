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
    using LiIoT.Models.Configuration;
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
            this._configFile = new ConfigFileModel();
        }

        /// <summary>
        /// Gets or sets configuration file.
        /// </summary>
        public ConfigFileModel ConfigFile
        {
            get { return this._configFile; }
            set { this._configFile = value; }
        }

#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable SA1300 // Element should begin with upper-case letter

        private static string _pathFolder => "liiot";

        private static string _pathFilename => "liiotdata.conf";

        /// <summary>
        /// Gets or sets configuration file.
        /// </summary>
        private ConfigFileModel _configFile { get; set; }

#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles

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
            #region Locate in run folder

            var tmpfile = new FileInfo(Path.Combine(Environment.CurrentDirectory, _pathFilename));

            if (tmpfile.Exists)
            {
                this.zzDebug = "dsfdsf";
                return tmpfile.FullName;
            }

            this.zzDebug = "dfdsf";

            #endregion

            #region Locate in Appdata local folder

            tmpfile = new FileInfo(Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), _pathFolder, _pathFilename));

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

        /// <summary>
        /// Save configuration file to disk.
        /// </summary>
        /// <param name="data">ConfigFileModel.</param>
        /// <param name="path">Where to save the file.</param>
        /// <returns>True if the file was saved.</returns>
        public bool ConfigurationFileSave(ConfigFileModel? data = null, string? path = null)
        {
            FileInfo tmpfile;

            // Check param data. if null get data from service.
            if (string.IsNullOrEmpty(@path))
            {
                // path = this._rundata.Folders.ConfigFile;
                tmpfile = new FileInfo(this._rundata.Folders.ConfigFile);
            }
            else
            {
                // Use path from input data.
                // Check if path exist?
                if (!Directory.Exists(@path))
                {
                    return false;
                }

                tmpfile = new FileInfo(Path.Combine(@path, _pathFilename));

                // path = new FileInfo(Path.Combine(path, "liiotdata.conf"));
            }

            if (data == null)
            {
                data = this._configFile;
            }

            // Turn model into json string.
            var tmpJsonString = LiTools.Helpers.Encoding.Json.Serialize(data, false);

            this.zzDebug = "SDfdsf";

            if (!LiTools.Helpers.IO.File.WriteFile(tmpfile, tmpJsonString, false))
            {
                // Error when saving file.
                return false;
            }

            this.zzDebug = "sdfdsf";

            return true;
        }

        public bool ConfigurationFileRead()
        {
            // ushort FileVersion = 0;

            var dd = LiTools.Helpers.IO.File.ReadTextFile(this._rundata.Folders.ConfigFile);
            if (!dd.Item1)
            {
                // Error reading configuration file
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                return false;
            }

            string tmpConfigFileAsString = dd.Item2;

            #region Get version information from string.

            if (tmpConfigFileAsString.Contains("Version"))
            {
                int hej = tmpConfigFileAsString.IndexOf("Version");

                // string tmpString = tmpConfigFileAsString.Substring(hej);
                string tmpString = tmpConfigFileAsString[hej..];

                if (tmpString.Contains(":") && tmpString.Contains(","))
                {
                    int tmpIdFirst = tmpString.IndexOf(":");
                    int tmpIdLast = tmpString.IndexOf(",");
                    string tmpVersionData = tmpString.Substring(tmpIdFirst + 1, tmpIdLast - tmpIdFirst - 1).Trim();

                    // Convert version string into uint16
                    try
                    {
                        LiIoT.Services.SoftwareRulesAndStaticData.ConfigFileVersion = ushort.Parse(tmpVersionData);

                        // Console.WriteLine(result);
                    }
                    catch (FormatException)
                    {
                        // Error get the version from string that is loaded from configfile.
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            System.Diagnostics.Debugger.Break();
                        }

                        return false;
                    }
                }
                else
                {
                    // Error get the version from string that is loaded from configfile. Missing syntax in file.
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        System.Diagnostics.Debugger.Break();
                    }

                    return false;
                }

                this.zzDebug = "sfdsf";
            }
            else
            {
                // Error get the version from string that is loaded from configfile. Missing syntax in file. "Version" is missing.
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                return false;
            }

            #endregion

            if (LiIoT.Services.SoftwareRulesAndStaticData.ConfigFileVersion < LiIoT.Services.SoftwareRulesAndStaticData.ConfigFileAtleastVersionToRun)
            {
                // Configfile is to old
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Break();
                }

                return false;
            }

            // Convert json string into model
            this._configFile = LiTools.Helpers.Encoding.Json.Deserialize<ConfigFileModel>(tmpConfigFileAsString);

            this.zzDebug = "dsfdsf";

            return true;
        }
    }
}
