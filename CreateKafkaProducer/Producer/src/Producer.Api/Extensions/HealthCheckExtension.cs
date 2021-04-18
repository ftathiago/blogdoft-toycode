using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Producer.WarmUp.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;

namespace Producer.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class HealthCheckExtension
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app) =>
            app
                .UseWarmUp("/startup")
                .UseHealthChecks(
                    "/healthcheck",
                    new HealthCheckOptions()
                    {
                        ResponseWriter = async (context, report) =>
                        {
                            var result = JsonSerializer.Serialize(
                                new
                                {
                                    statusApplication = report.Status.ToString(),
                                    healthChecks = report.Entries.Select(e => new
                                    {
                                        check = e.Key,
                                        ErrorMessage = e.Value.Exception?.Message,
                                        status = Enum.GetName(typeof(HealthStatus), e.Value.Status),
                                    }),
                                });
                            context.Response.ContentType = MediaTypeNames.Application.Json;
                            await context.Response.WriteAsync(result);
                        },
                    });
    }
}
