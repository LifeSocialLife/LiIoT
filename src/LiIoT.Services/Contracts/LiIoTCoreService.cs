// <summary>
// LiIoTCoreService.
// </summary>
// <copyright file="LiIoTCoreService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// LiIoTCoreService - Interface for core services whit start command.
    /// </summary>
    public abstract class LiIoTCoreService
    {
        /// <summary>
        /// Start command.
        /// </summary>
        public void Start()
        {
            this.OnStart();
        }

        /// <summary>
        /// OnStart.
        /// </summary>
        protected virtual void OnStart()
        {
        }
    }
}
