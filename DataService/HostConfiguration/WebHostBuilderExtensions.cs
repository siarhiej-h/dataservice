using System.IO;
using Common.Logging;
using Common.Logging.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DataService.HostConfiguration
{
    internal static class WebHostBuilderExtensions
    {
        private static IConfiguration configuration_;

        private static IConfiguration Configuration
        {
            get { return configuration_ ?? (configuration_ = Configure()); }
        }

        public static IWebHostBuilder UseCommonLogging(this IWebHostBuilder builder)
        {
            LogConfiguration logConfiguration = new LogConfiguration();
            Configuration.GetSection("LogConfiguration").Bind(logConfiguration);
            LogManager.Configure(logConfiguration);

            return builder;
        }

        public static IWebHostBuilder UseUrls(this IWebHostBuilder builder)
        {
            builder.UseUrls(Configuration.GetValue<string>("server.urls"));

            return builder;
        }

        private static IConfiguration Configure()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
        }
    }
}