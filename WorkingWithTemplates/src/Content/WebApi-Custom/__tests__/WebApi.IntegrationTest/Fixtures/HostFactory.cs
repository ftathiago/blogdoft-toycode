using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WebApi.InfraData.Contexts;
using WebApi.IntegrationTest.Fixtures.DataFixtures;

namespace WebApi.IntegrationTest.Fixtures
{
    public class HostFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbConnectionFactory));
                services.Remove(descriptor);
                services.AddScoped<IDbConnectionFactory, SqLiteConnectionFactory>();
            });
        }
    }
}