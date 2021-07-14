// <summary>
// Starter no Gui Worker class.
// </summary>
// <copyright file="Program.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace StarterNoGui
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LiIoT.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Main starter for No gui run of the sortware.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main starter no Gui.
        /// </summary>
        /// <param name="args">starting commands.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Build services and starting data.
        /// </summary>
        /// <param name="args">starting commands.</param>
        /// <returns>nodata.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<RunDataService>();
                    services.AddSingleton<ConfigFileService>();

                    LiIoT.Services.SoftwareRulesAndStaticData.PathRuntimes = hostContext.Configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
                    services.AddHostedService<LiIoT.Services.Worker>();
                });
    }
}
