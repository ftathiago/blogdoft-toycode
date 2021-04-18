using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Producer.Api.Extensions
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
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
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
