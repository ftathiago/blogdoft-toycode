using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
#if (!excludeSamples)
using WebApi.Business.Repositories;
#endif
using WebApi.InfraData.Contexts;
using WebApi.InfraData.Contexts.Impl;
#if (!excludeSamples)
using WebApi.InfraData.Repositories;
#endif
using WebApi.Shared.Data.Contexts;

namespace WebApi.InfraData.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services) =>
            services
#if (!excludeSamples)
                .AddScoped<ISampleRepository, SampleRepository>()
#endif
                .AddScoped<IDbConnectionFactory, DbConnectionFactory>()
                .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}