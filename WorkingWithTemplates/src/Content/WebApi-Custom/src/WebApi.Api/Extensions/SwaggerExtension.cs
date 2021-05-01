using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using WebApi.Api.Configurations;

namespace WebApi.Api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigSwagger(this IServiceCollection services) => services
            .AddSwaggerGen(s =>
            {
                /*********************************************************
                 Uncomment if you need a Bearer token authentication

                 s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Input the JWT like: Bearer {your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                 });
                 *********************************************************/
                s.ExampleFilters();
            })
            .AddSwaggerExamplesFromAssemblyOf<Startup>()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
    }
}
