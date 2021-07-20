// <summary>
// Device Item update model.
// </summary>
// <copyright file="DeviceItemUpdateModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Device Items (relay, sensor) update model.
    /// </summary>
    public class DeviceItemUpdateModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceItemUpdateModel"/> class.
        /// </summary>
        public DeviceItemUpdateModel()
        {
            this.ItemId = string.Empty;
            this.ItemValue = string.Empty;
            this.ItemDtvalue = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets relay or sensor that shod be updated.
        /// </summary>
        public DeviceEnums.DeviceItemsTypes Itemtype { get; set; }

        /// <summary>
        /// Gets or sets item id to update.
        /// </summary>
        public string ItemId { get; set; }

        /// <summary>
        /// Gets or sets new value to set on the itemId.
        /// </summary>
        public string ItemValue { get; set; }

        /// <summary>
        /// Gets or sets datetime when the value was updated.
        /// </summary>
        public DateTime ItemDtvalue { get; set; }
    }
}
