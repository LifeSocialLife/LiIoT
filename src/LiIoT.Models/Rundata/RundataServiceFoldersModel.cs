// <summary>
// Storage pool work.
// </summary>
// <copyright file="RundataServiceFoldersModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Models.Rundata
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Model rundata service folders that the software is using.
    /// </summary>
    public class RundataServiceFoldersModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RundataServiceFoldersModel"/> class.
        /// </summary>
        public RundataServiceFoldersModel()
        {
            this.PathRuntimes = string.Empty;
            this.PathExecuteFile = string.Empty;
            this.PathExecute = string.Empty;
            this.ConfigFile = string.Empty;
            this.PathData = string.Empty;
        }

        /// <summary>
        /// Gets or sets Path from where the software is started.
        /// </summary>
        public string PathRuntimes { get; set; }

        /// <summary>
        /// Gets or sets Path to the software starting file. ex "C:\\...\\src\\StarterGui\\bin\\Debug\\net6.0\\StarterGui.exe".
        /// </summary>
        public string PathExecuteFile { get; set; }

        /// <summary>
        /// Gets or sets Path to the software folder. same as PathExecuteFile whitout fileinformation.
        /// </summary>
        public string PathExecute { get; set; }

        /// <summary>
        /// Gets or sets path to configuration file.
        /// </summary>
        public string ConfigFile { get; set; }

        /// <summary>
        /// Gets or sets path to Data folder.
        /// </summary>
        public string PathData { get; set; }

        /// <summary>
        /// Return all saved data in this model.
        /// </summary>
        /// <returns>dic modelname and value.</returns>
        public Dictionary<string, string> GetAllData()
        {
            var aa = new Dictionary<string, string>();
            aa.Add("PathRuntimes", this.PathRuntimes);
            aa.Add("PathExecuteFile", this.PathExecuteFile);
            aa.Add("PathExecute", this.PathExecute);
            aa.Add("ConfigFile", this.ConfigFile);
            aa.Add("DataFolder", this.PathData);
            return aa;
        }
    }
}
