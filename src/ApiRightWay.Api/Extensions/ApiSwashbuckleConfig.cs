using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ApiRightWay.Extensions;

internal static class ApiSwashbuckleConfig
{
    public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app) =>
        app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
                // Geração de um endpoint do Swagger para cada versão descoberta
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
}
