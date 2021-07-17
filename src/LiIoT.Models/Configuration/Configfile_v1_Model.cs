// <summary>
// Configuration file model. Configfile_v1_Model.
// </summary>
// <copyright file="Configfile_v1_Model.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Models.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Configuration file saved on disk running this software. Version 1.
    /// </summary>
    public class Configfile_v1_Model
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configfile_v1_Model"/> class.
        /// </summary>
        public Configfile_v1_Model()
        {
            this.NodeName = string.Empty;
            this.Version = 1;
            this.NodeInfo = string.Empty;
            this.EncryptDataFolder = false;
            this.PathData = string.Empty;
        }

        /// <summary>
        /// Gets or sets name of this node running this.
        /// </summary>
        [Required(ErrorMessage = "This node need to have a name!!")]
        [MinLength(5, ErrorMessage = "Name is too short. min 5 characters.")]
        [MaxLength(20, ErrorMessage = "Name is too long. max 20 characters.")]
        public string NodeName { get; set; }

        /// <summary>
        /// Gets or sets version  of this model in use.
        /// </summary>
        public ushort Version { get; set; }

        /// <summary>
        /// Gets or sets nice name, Description of this node running this software.
        /// </summary>
        [Required(ErrorMessage = "This node need to have a description!!")]
        [MinLength(5, ErrorMessage = "Name is too short. min 5 characters.")]
        [MaxLength(99, ErrorMessage = "Name is too long. max 99 characters.")]
        public string NodeInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether shod all configuration files inside Data folder by encrypted.
        /// </summary>
        public bool EncryptDataFolder { get; set; }

        /// <summary>
        /// Gets or sets path to data files.
        /// </summary>
        [Required(ErrorMessage = "You need to type a folder (path) where files shod be saved.!!")]
        public string PathData { get; set; }
    }
}
