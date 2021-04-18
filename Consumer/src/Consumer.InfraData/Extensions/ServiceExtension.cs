using Microsoft.Extensions.DependencyInjection;

namespace Consumer.InfraData.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddInfraData(this IServiceCollection services) =>
            services;
    }
}
