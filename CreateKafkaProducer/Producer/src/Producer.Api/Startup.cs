using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Producer.Api.Extensions;
using Producer.Api.Middlerwares;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Producer
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ControllerExceptionFilter>();
                options.Filters.Add<MessageFilter>();
            });

            services.AddHealthChecks();

            services.ConfigureIoc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseTelemetry(Configuration);

            Console.WriteLine(Configuration["ElasticApm:ServerUrl"]);

            if (env.IsDevelopment())
            {
                app
                    .UseDeveloperExceptionPage();
            }

            app
                .ConfigureSwagger(provider)
                .UseRouting()
                .UseHealthCheck()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
