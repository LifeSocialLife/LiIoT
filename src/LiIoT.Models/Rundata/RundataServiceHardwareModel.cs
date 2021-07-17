// <summary>
// Storage pool work.
// </summary>
// <copyright file="RundataServiceHardwareModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Models.Rundata
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using static LiTools.Helpers.IO.Hardware;

    /// <summary>
    /// Rundata Service Hardware model.
    /// </summary>
    public class RundataServiceHardwareModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RundataServiceHardwareModel"/> class.
        /// </summary>
        public RundataServiceHardwareModel()
        {
            this.Os = PlatformOsEnum.None;
            this.FrameworkDescription = string.Empty;
            this.OSArchitecture = ArchitectureEnum.None;
            this.ProcessorArchitecture = ArchitectureEnum.None;
            this.OsDescription = string.Empty;
        }

        /// <summary>
        /// Gets or sets os this is running on. Windows, linux, osv.
        /// </summary>
        public PlatformOsEnum Os { get; set; }

        /// <summary>
        /// Gets or sets what is the os architecture. x86 or x64 ??.
        /// </summary>
        public ArchitectureEnum OSArchitecture { get; set; }

        /// <summary>
        /// Gets or sets os Description. this contains os version if running on windows.
        /// </summary>
        public string OsDescription { get; set; }

        /// <summary>
        /// Gets or sets framework information. dotnet framwork information.
        /// </summary>
        public string FrameworkDescription { get; set; }

        /// <summary>
        /// Gets or sets what is the CPU (processor) architecure. X86 or X64 ??.
        /// </summary>
        public ArchitectureEnum ProcessorArchitecture { get; set; }

        /// <summary>
        /// Get all data from this model as dict.
        /// </summary>
        /// <returns>dict modelname and value.</returns>
        public Dictionary<string, string> GetAllData()
        {
            var aa = new Dictionary<string, string>();
            aa.Add("Os", this.Os.ToString());
            aa.Add("OSArchitecture", this.OSArchitecture.ToString());
            aa.Add("OsDescription", this.OsDescription);
            aa.Add("FrameworkDescription", this.FrameworkDescription);
            aa.Add("ProcesArchitecture", this.ProcessorArchitecture.ToString());
            return aa;
        }
    }
}
