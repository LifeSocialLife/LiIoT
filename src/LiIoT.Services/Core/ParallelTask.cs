// <summary>
// ParallelTask - Handling all Task running on system.
// </summary>
// <copyright file="ParallelTask.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Core
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Task helper to start diffrent tasks.
    /// </summary>
    public static class ParallelTask
    {
        /// <summary>
        /// Start Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="cancellationToken">Token.</param>
        public static void Start(Func<Task> action, ILogger logger, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Task.Factory.StartNew(
                action,
                cancellationToken,
                TaskCreationOptions.None,
                TaskScheduler.Default).ContinueWith(
                t =>
                {
                    logger.LogWarning(t.Exception, "Error while executing a parallel task.");
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// Start longrunning Task.
        /// </summary>
        /// <param name="action">Function to run.</param>
        /// <param name="logger">ILogger.</param>
        /// <param name="cancellationToken">Token.</param>
        public static void StartLongRunning(Action action, ILogger logger, CancellationToken cancellationToken)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            Task.Factory.StartNew(
                action,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default).ContinueWith(
                t =>
                {
                    logger.LogWarning(t.Exception, "Error while executing a parallel task.");
                },
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
