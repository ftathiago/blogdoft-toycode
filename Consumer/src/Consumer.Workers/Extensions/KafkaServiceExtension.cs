using Confluent.Kafka;
using Consumer.Business.Model;
using Consumer.Business.Services;
using Consumer.Workers.Configurations;
using Consumer.Workers.Providers;
using Consumer.Workers.Serializers;
using Consumer.Workers.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consumer.Workers.Extensions
{
    public static class KafkaServiceExtension
    {
        public static IServiceCollection AddKafka(this IServiceCollection services) =>
            services
                .AddTransient(_ => Deserializers.Null)
                .AddTransient<IDeserializer<SaleEvent>, SaleEventDeserializer>()
                .AddScoped<IEventReceiver, EventReceiver>()
                .AddScoped(typeof(IConsumerProvider<,>), typeof(ConsumerProvider<,>))
                .AddConsumerConfiguration();

        private static IServiceCollection AddConsumerConfiguration(this IServiceCollection services) =>
            services
                .AddTransient(provider => provider
                    .GetService<IConfiguration>()
                    .GetSection(nameof(ConsumerConfiguration))?
                    .Get<ConsumerConfiguration>());
    }
}
