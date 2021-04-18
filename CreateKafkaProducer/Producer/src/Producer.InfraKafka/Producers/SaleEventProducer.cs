using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Producer.Business.Entities;
using Producer.Business.Services;
using Producer.InfraKafka.Providers;
using Producer.Shared.Holders;
using Producer.WarmUp.Abstractions;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Producer.InfraKafka.Producers
{
    public class SaleEventProducer : IEvent, IWarmUpCommand
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topicName;
        private readonly IHttpContextAccessor _context;

        public SaleEventProducer(
            IProducerProvider<Null, string> provider,
            IConfiguration configuration,
            IHttpContextAccessor context)
        {
            _producer = provider.GetProducer();
            _topicName = configuration.GetSection("ProducerConfig").GetValue<string>("Topic");
            _context = context;
        }

        public async Task PublishAsync(SaleEntity entity)
        {
            var message = BuildMessageWith(entity);
            await _producer.ProduceAsync(_topicName, message);
        }

        Task IWarmUpCommand.Execute() =>
            Task.CompletedTask;

        private static string SerializeEntity(SaleEntity entity) => JsonSerializer.Serialize(
            entity,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        private Message<Null, string> BuildMessageWith(SaleEntity entity) => new()
        {
            Value = SerializeEntity(entity),
            Headers = BuildHeaders(),
        };

        private Headers BuildHeaders() => new()
        {
            new Header("correlationId", Encoding.UTF8.GetBytes(_context.CorrelationId.ToString())),
        };
    }
}
