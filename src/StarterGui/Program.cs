// <summary>
// Gui Starter.
// </summary>
// <copyright file="Program.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>

namespace StarterGui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LiIoT.Services.Core;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Starter Gui Main program starter class.
    /// </summary>
    public class Program
    {
        private static readonly SystemCancellationTokenService SystemCancellationToken = new SystemCancellationTokenService();

        /// <summary>
        /// Main Starter of Gui starter.
        /// </summary>
        /// <param name="args">Read starting arg.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            // CreateHostBuilder(args).Build().Run();
            // -
            using (var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureServices(s =>
                    {
                        s.AddSingleton(SystemCancellationToken);
                    });
                }).Build())
            {
                await host.RunAsync(SystemCancellationToken.Token).ConfigureAwait(false);

                SystemCancellationToken?.Cancel();
            }
        }

        /*
        /// <summary>
        /// Host Creater.
        /// </summary>
        /// <param name="args">starting variables.</param>
        /// <returns>no data.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureServices(s =>
                    {
                        s.AddSingleton(SystemCancellationToken);
                    });
                });
        */
    }
}
