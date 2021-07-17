// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="SoftwareRulesAndStaticData.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services
{
    /// <summary>
    /// Minimus rules and static data for software to run.
    /// </summary>
    public static class SoftwareRulesAndStaticData
    {
        /// <summary>
        /// Gets version.
        /// </summary>
        public static string Version => "0.1";

        /// <summary>
        /// Gets version Build date and information.
        /// </summary>
        public static string VersionBuild => "BETA 202107715";

        /// <summary>
        /// Gets or sets temp folder to pathruntimes from starting project befor service is activated.
        /// </summary>
        public static string? PathRuntimes { get; set; }

        /// <summary>
        /// Gets configfile need to be atlest this version for this software to run.
        /// </summary>
        public static ushort ConfigFileAtleastVersionToRun => 1;

        /// <summary>
        /// Gets configfile latest version.
        /// </summary>
        public static ushort ConfigFileLatestVersion => 1;

        /// <summary>
        /// Gets or sets the version of the configfile that is running now.
        /// </summary>
        public static ushort ConfigFileVersion { get; set; }

        /// <summary>
        /// Gets folder where configuration file can be saved.
        /// </summary>
        public static string PathFoldername => "liiot";

        /// <summary>
        /// Gets name of main configuration filename.
        /// </summary>
        public static string ConfigurationFilename => "liiotdata.conf";

        /// <summary>
        /// Gets db filename.
        /// </summary>
        public static string DbFilename => "liiotdb.db";
    }
}
