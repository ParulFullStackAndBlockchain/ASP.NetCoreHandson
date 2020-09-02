using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     // Remove all the default logging providers
                     //logging.ClearProviders();

                     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

                     logging.AddConsole();
                     logging.AddDebug();
                     logging.AddEventSourceLogger();
                     // Enable NLog as one of the Logging Provider
                     logging.AddNLog();
                 })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //As part of setting up a web host, Startup class is also configured using the UseStartup() extension
                    //method of IWebHostBuilder class.

                    //Extention method example : List<int> Numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                    //IEnumerable<int> EvenNumbers = Numbers.Where(n => n % 2 == 0);
                    //In spite of Where() method not belonging to List<T> class, we are still able to use it as though it 
                    //belong to List<T> class. This is possible because Where() method is implemented as extension method
                    //in IEnumerable<T> interface and List<T> implements IEnumerable<T> interface.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
