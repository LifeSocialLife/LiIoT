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
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Starter Gui Main program starter class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Starter of Gui starter.
        /// </summary>
        /// <param name="args">Read starting arg.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

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
                });
    }
}
