// <summary>
// NodesConfigfileItemsModel.
// </summary>
// <copyright file="NodesConfigfileItemsModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Models.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Nodes configuration file model.
    /// </summary>
    public class NodesConfigfileItemsModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodesConfigfileItemsModel"/> class.
        /// </summary>
        public NodesConfigfileItemsModel()
        {
            this.Ip = string.Empty;
            this.PortHub = 0;
            this.PortApi = 0;
            this.Enabled = false;
        }

        /// <summary>
        /// Gets or sets ip to node.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets port to Hub interface.
        /// </summary>
        public ushort PortHub { get; set; }

        /// <summary>
        /// Gets or sets port to Api interface.
        /// </summary>
        public ushort PortApi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is this node enabled.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
