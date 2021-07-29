// <summary>
// Cancellation Token Service.
// </summary>
// <copyright file="SystemCancellationTokenService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Cancellation Token Service.
    /// </summary>
    public sealed class SystemCancellationTokenService
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:FieldNamesMustNotBeginWithUnderscore", Justification = "Reviewed.")]
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemCancellationTokenService"/> class.
        /// </summary>
        public SystemCancellationTokenService()
        {
            this.Token = this._cancellationTokenSource.Token;
        }

        /// <summary>
        /// Gets token data.
        /// </summary>
        public CancellationToken Token { get; }

        /// <summary>
        /// Cancel request sent to token.
        /// </summary>
        public void Cancel()
        {
            this._cancellationTokenSource.Cancel(false);
        }
    }
}
