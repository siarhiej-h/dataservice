using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace DataService
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls("http://localhost:4242")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
