using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Producer.Business.Services;
using Producer.InfraData.Contexts;
using Producer.IntegrationTest.Fixtures.DataFixtures;
using System.Linq;

namespace Producer.IntegrationTest.Fixtures
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
                services.AddScoped<IProducer, ProducerFixture>();
            });
        }
    }
}
