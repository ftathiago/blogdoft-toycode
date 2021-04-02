using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
#if (!excludeSamples)
using WebApi.Business.Services;
#endif

namespace WebApi.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) =>
#if (!excludeSamples)
            services
                .AddScoped<ISampleService, SampleService>();
#endif
#if (excludeSamples)
            services;
#endif
    }
}