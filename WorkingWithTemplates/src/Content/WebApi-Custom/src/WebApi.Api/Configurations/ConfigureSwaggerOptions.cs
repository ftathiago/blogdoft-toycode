using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace WebApi.Api.Configurations
{
    [ExcludeFromCodeCoverage]
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) =>
            _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            _provider.ApiVersionDescriptions
                .ToList()
                .ForEach(d =>
                {
                    var info = new OpenApiInfo
                    {
                        Title = "WebApi.",
                        Version = d.ApiVersion.ToString(),
                        Description = d.IsDeprecated ? "Deprecated" : string.Empty,
                    };

                    options.SwaggerDoc(d.GroupName, info);
                });
        }
    }
}
