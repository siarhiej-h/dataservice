using DataService.HostConfiguration;
using Microsoft.AspNetCore.Hosting;

namespace DataService
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseCommonLogging()
                .UseUrls()
                .UseStartup<Startup>()
                .UseKestrel()
                .Build();

            host.Run();
        }
    }
}
