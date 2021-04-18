using Consumer.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Workers.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddIoC(this IServiceCollection services) =>
            services
                .AddHostedService<Worker>()
                .AddKafka()
                .ConfigureIoC();
    }
}
