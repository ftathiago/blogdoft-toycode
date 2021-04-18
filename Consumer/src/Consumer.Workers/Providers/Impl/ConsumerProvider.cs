using Confluent.Kafka;
using Consumer.Workers.Configurations;
using Microsoft.Extensions.Logging;

namespace Consumer.Workers.Providers
{
    internal class ConsumerProvider<TKey, TValue> : IConsumerProvider<TKey, TValue>
    {
        private readonly IDeserializer<TKey> _keyDeserializer;
        private readonly IDeserializer<TValue> _valueDeserializer;
        private readonly ConsumerConfiguration _configuration;
        private readonly ILogger<ConsumerProvider<TKey, TValue>> _logger;
        private IConsumer<TKey, TValue> _consumer;
        private bool _disposedValue;

        public ConsumerProvider(
            IDeserializer<TKey> keyDeserializer,
            IDeserializer<TValue> valueDeserializer,
            ConsumerConfiguration configuration,
            ILogger<ConsumerProvider<TKey, TValue>> logger)
        {
            _keyDeserializer = keyDeserializer;
            _valueDeserializer = valueDeserializer;
            _configuration = configuration;
            _logger = logger;
        }

        public IConsumer<TKey, TValue> GetConsumer() =>
            _consumer ?? BuildConsumer();

        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _consumer?.Dispose();
                }

                _disposedValue = true;
            }
        }

        private IConsumer<TKey, TValue> BuildConsumer()
        {
            if (_consumer is not null)
            {
                return _consumer;
            }

            _consumer = new ConsumerBuilder<TKey, TValue>(_configuration)
                .SetErrorHandler((_, error) => _logger.LogError(error.Reason))
                .SetLogHandler((_, logMessage) => _logger.LogInformation(logMessage.Message))
                .SetKeyDeserializer(_keyDeserializer)
                .SetValueDeserializer(_valueDeserializer)
                .Build();
            _consumer.Subscribe(_configuration.Topic);

            return _consumer;
        }
    }
}
