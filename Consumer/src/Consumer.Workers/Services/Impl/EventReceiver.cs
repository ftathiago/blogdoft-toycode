using Confluent.Kafka;
using Consumer.Business.Model;
using Consumer.Business.Services;
using Consumer.InfraKafka.Extensions;
using Consumer.Workers.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.Workers.Services
{
    internal class EventReceiver : IEventReceiver
    {
        private readonly IConsumer<Null, SaleEvent> _consumer;

        public EventReceiver(IConsumerProvider<Null, SaleEvent> provider) =>
            _consumer = provider.GetConsumer();

        public async Task Execute(
            Func<IDictionary<string, string>, SaleEvent, Task> messageHandler,
            CancellationToken token = default)
        {
            var consumerResult = await Task.Run(() => _consumer.Consume(token));

            await messageHandler(
                consumerResult.Message.Headers.ToDictionary(),
                consumerResult.Message.Value);

            _consumer.StoreOffset(consumerResult);
            _consumer.Commit();
        }
    }
}
