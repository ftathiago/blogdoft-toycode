using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;

namespace Producer.Api.Extensions
{
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
