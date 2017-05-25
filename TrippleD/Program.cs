using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TrippleD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            IWebHost host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}