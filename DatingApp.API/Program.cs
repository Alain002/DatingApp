using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApp.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        // public static void Main(string[] args)
        // {
        //     var hosts = CreateHostBuilder(args).Build();

        //     using(var scope = hosts.Services.CreateScope()) {
        //         var services = scope.ServiceProvider;
        //         try
        //         {
        //             var context = services.GetRequiredService<DataContext>();
        //             context.Database.Migrate();
        //             Seed.SeedUsers(context);

        //         } catch(Exception ex)
        //         {
        //             var logger = services.GetRequiredService<ILogger<Program>>();
        //             logger.LogError(ex, "An error occured during migration");
        //         }
        //     }
        // }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
