using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Producer.Api.Configurations
{
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
                        Title = "Producer example.",
                        Version = d.ApiVersion.ToString(),
                        Description = d.IsDeprecated ? "Deprecated" : string.Empty,
                    };

                    options.SwaggerDoc(d.GroupName, info);
                });
        }
    }
}
