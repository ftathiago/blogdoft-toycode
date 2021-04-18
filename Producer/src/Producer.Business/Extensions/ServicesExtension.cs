using Microsoft.Extensions.DependencyInjection;
using Producer.Business.Services;
using System.Diagnostics.CodeAnalysis;

namespace Producer.Business.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) =>
            services
                .AddScoped<ISaleService, SalesService>();
    }
}
