using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;//To use AddNLog

namespace AC_EmpManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //           .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .ConfigureLogging((hostingContext, loggingBuilder) =>
                   {
                       loggingBuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                       loggingBuilder.AddConsole();
                       loggingBuilder.AddDebug();
                       loggingBuilder.AddEventSourceLogger();
                       loggingBuilder.AddNLog();
                   })
                   .UseStartup<Startup>();
    }
}
