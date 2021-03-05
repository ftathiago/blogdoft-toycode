using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using WebApi.Api.Extensions;
using WebApi.Api.Lib;

namespace WebApi
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static void Main(string[] args)
        {
            LogConfigBuilder.AutoWire();
            try
            {
                CreateHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal($"Failed to start the {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => config.SetupSource())
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
            .UseSerilog();
    }
}
