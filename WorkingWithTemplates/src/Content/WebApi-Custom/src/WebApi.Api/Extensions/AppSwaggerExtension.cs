using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebApi.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AppSwaggerExtension
    {
        public static IApplicationBuilder ConfigureSwagger(
            this IApplicationBuilder app,
            IApiVersionDescriptionProvider provider) =>
            app
                .UseSwagger()
                .UseSwaggerUI(o => provider.ApiVersionDescriptions
                    .ToList()
                    .ForEach(d =>
                        o.SwaggerEndpoint($"/swagger/{d.GroupName}/swagger.json", d.GroupName.ToUpper())));
    }
}
