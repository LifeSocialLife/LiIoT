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
    }
}
