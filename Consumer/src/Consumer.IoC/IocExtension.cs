using Consumer.Business.Extensions;
using Consumer.InfraData.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.IoC
{
    public static class IocExtension
    {
        public static IServiceCollection ConfigureIoC(this IServiceCollection services) =>
            services
                .AddBusiness()
                .AddInfraData();
    }
}
