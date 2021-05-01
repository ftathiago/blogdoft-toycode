using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ConfigureAppVersioning
    {
        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services) =>
            services
                .AddApiVersioning(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                })
                .AddVersionedApiExplorer(options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.GroupNameFormat = "'version - 'VVVV";
                    options.SubstituteApiVersionInUrl = true;
                });
    }
}
