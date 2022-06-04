using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sql;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_FileCloud.DataLayer.DB;
using WebApi_FileCloud.DataLayer.Services;

namespace WebApi_FileCloud
{
    public class Program22

    {
        public static async Task Main(string[] args)
        {
            Account.ConnectionString = @"Server=.\SQLEXPRESS;Database=FileCloud;Trusted_Connection=True;";

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
