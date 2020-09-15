using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace intersecting_rectangles
{
    class Program
    {
        private static void PrintHelp()
        {
            Console.WriteLine("Usage: intersect.exe <a.json>");
        }
        private static bool CheckArgsAndPrintHelp(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid number of arguments");
                PrintHelp();
                return false;
            }
            var fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File not found");
                PrintHelp();
                return false;
            }
            return true;
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                //services.AddLogging(configure => configure.AddConsole()); default Builder already adds the console
                services.AddTransient<MyApplication>();
            });

        static async Task<int> Main(string[] args)
        {
            if (!CheckArgsAndPrintHelp(args)) return -1;
            var fileName = args[0];

            //setup host with Dependency Injection
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                var _logger = services.GetService<ILogger<Program>>();

                try
                {
                    var myService = services.GetRequiredService<MyApplication>();
                    await myService.Run(fileName);

                    //_logger.LogInformation("Success");
                    return 0;
                }
                catch (Exception ex)
                {                    
                    _logger.LogCritical(ex,ex.Message);
                    return -1;
                }
            }
        }
    }
}
