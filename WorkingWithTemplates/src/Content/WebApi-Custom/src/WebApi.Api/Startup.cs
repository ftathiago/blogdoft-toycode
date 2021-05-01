using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using WebApi.Api.Extensions;
using WebApi.Api.Middlerwares;
using WebApi.IoC;

namespace WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ControllerExceptionFilter>();
                options.Filters.Add<MessageFilter>();
            });
            services.AddHealthChecks();

            services
                .ConfigSwagger()
                .ConfigureApiVersioning()
                .ProjectsIocConfig();
        }

        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IApiVersionDescriptionProvider apiVersionDescription)
        {
            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage()
                    .ConfigureSwagger(apiVersionDescription);
            }

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseHealthCheck()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
