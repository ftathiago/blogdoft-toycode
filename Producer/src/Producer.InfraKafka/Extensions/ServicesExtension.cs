using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Producer.Business.Services;
using Producer.InfraKafka.Configurations;
using Producer.InfraKafka.Producers;
using Producer.InfraKafka.Providers;

namespace Producer.InfraKafka.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddInfraKafka(
            this IServiceCollection services) => services
            .AddKafkaConnectionConfiguration()
            .AddProducerConfig();

        private static IServiceCollection AddKafkaConnectionConfiguration(
            this IServiceCollection services) =>
            services
                .AddSingleton(typeof(IProducerProvider<,>), typeof(ProducerProvider<,>))
                .AddScoped<IEvent, SaleEventProducer>();

        private static IServiceCollection AddProducerConfig(
            this IServiceCollection services) => services
            .AddSingleton(provider => provider
                .GetService<IConfiguration>()
                .GetSection("ProducerConfig")
                .Get<KafkaProducerConfig>());
    }
}
