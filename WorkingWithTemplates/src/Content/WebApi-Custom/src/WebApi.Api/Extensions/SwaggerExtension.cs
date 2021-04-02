using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigSwagger(this IServiceCollection services) => services
            .AddSwaggerGen(c => c.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "Api",
                    Version = "v1",
                }));
    }
}