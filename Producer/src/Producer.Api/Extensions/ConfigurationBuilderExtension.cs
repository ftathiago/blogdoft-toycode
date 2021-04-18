using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Producer.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ConfigurationBuilderExtension
    {
        private const string EnvDevelopment = "Development";

        public static IConfigurationBuilder SetupSource(this IConfigurationBuilder configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? EnvDevelopment;
            if (environment == EnvDevelopment)
            {
                configuration
                    .AddUserSecrets(Assembly.GetExecutingAssembly())
                    .AddJsonFile($"appsettings.{environment}.json", optional: true);
            }

            return configuration
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}
