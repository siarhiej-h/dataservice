using System;
using Common.Logging;
using Common.Logging.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DataService.HostConfiguration
{
    internal static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCommonLogging(this IWebHostBuilder builder)
        {
            LogConfiguration logConfiguration = new LogConfiguration();
            ConfigurationManager.GetSection("LogConfiguration").Bind(logConfiguration);
            LogManager.Configure(logConfiguration);

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                LogManager.GetLogger(sender.GetType()).Fatal(args.ExceptionObject);
            };

            return builder;
        }

        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder)
        {
            builder.UseUrls(ConfigurationManager.GetValue<string>("server.urls"));

            return builder;
        }
    }
}