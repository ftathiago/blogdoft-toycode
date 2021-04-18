using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Producer.Business.Repositories;
using Producer.Business.Services;
using Producer.InfraData.Caching;
using Producer.InfraData.Contexts;
using Producer.InfraData.Models;
using Producer.InfraData.Repositories;
using Producer.Shared.Data.Contexts;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Producer.InfraData.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services) =>
            services
                .AddScoped<ISaleRepository, SalesRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IDbConnectionFactory, DbConnectionFactory>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddTransient(typeof(ICaching<ProductTable, Guid>), typeof(Caching<ProductTable, Guid>))
                .AddTransient<ICacheLoader, CachingLoader>()
                .AddTransient<ILookupCacheLoader<ProductTable>, ProductRepository>()
                .AddSingleton<IMemoryCache, MemoryCache>();
    }
}
