using Consumer.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Business.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services) =>
            services
                .AddScoped<ISaleEventHandler, SaleEventHandler>();
    }
}
