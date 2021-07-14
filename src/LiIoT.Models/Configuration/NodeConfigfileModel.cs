// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="NodeConfigfileModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Models.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary>
    /// Node configuration file. information about other nodes in system.
    /// </summary>
    public class NodeConfigfileModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NodeConfigfileModel"/> class.
        /// </summary>
        public NodeConfigfileModel()
        {
            this.Nodes = new List<NodesConfigfileItemsModel>();
            this.Version = 1;
        }

        /// <summary>
        /// Gets or sets information about other nodes in system.
        /// </summary>
        public List<NodesConfigfileItemsModel> Nodes { get; set; }

        /// <summary>
        /// Gets or sets version of this file.
        /// </summary>
        public ushort Version { get; set; }
    }
}
