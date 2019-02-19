using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Common.Logging;
using Common.Logging.Configuration;
using Nancy.Owin;

namespace DataService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(env.ContentRootPath)
                .Build();

            ConfigureLogger(config);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new CustomBootstrapper()));
        }

        private void ConfigureLogger(IConfigurationRoot config)
        {
            LogConfiguration logConfiguration = new LogConfiguration();
            config.GetSection("LogConfiguration").Bind(logConfiguration);
            LogManager.Configure(logConfiguration);
        }
    }
}
