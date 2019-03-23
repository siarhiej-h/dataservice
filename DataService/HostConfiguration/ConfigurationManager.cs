using System.IO;
using Microsoft.Extensions.Configuration;

namespace DataService.HostConfiguration
{
    internal static class ConfigurationManager
    {
        private static IConfiguration configuration_;

        private static IConfiguration Configuration
        {
            get { return configuration_ ?? (configuration_ = Configure()); }
        }

        private static IConfiguration Configure()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
        }

        public static IConfigurationSection GetSection(string key)
        {
            return Configuration.GetSection(key);
        }

        public static T GetValue<T>(string key)
        {
            return Configuration.GetValue<T>(key);
        }
    }
}
