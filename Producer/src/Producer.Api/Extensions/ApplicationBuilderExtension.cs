using Elastic.Apm.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Producer.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseTelemetry(this IApplicationBuilder app, IConfiguration configuration)
        {
            var validProperty = bool.TryParse(configuration["ElasticApm:UseTelemetry"], out var useTelemetry);
            useTelemetry = validProperty && useTelemetry;

            if (useTelemetry)
            {
                app.UseElasticApm(configuration);
            }

            return app;
        }
    }
}
