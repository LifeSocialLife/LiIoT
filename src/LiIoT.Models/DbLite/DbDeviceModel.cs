// <summary>
// LiteDb Model Device
// </summary>
// <copyright file="DbDeviceModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Models.DbLite
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using LiteDB;

    /// <summary>
    /// Device model Db Lite.
    /// </summary>
    public class DbDeviceModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbDeviceModel"/> class.
        /// </summary>
        public DbDeviceModel()
        {
            this.Id = new ObjectId();
            this.Text = string.Empty;
        }

        /// <summary>
        /// Gets or sets id for this post.
        /// </summary>
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets or sets information string.
        /// </summary>
        public string Text { get; set; }
    }
}
