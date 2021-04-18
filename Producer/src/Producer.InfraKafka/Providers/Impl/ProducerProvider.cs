using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Producer.InfraKafka.Exceptions;
using Producer.InfraKafka.Extensions;
using System;

namespace Producer.InfraKafka.Providers
{
    public class ProducerProvider<TKey, TValue> : IProducerProvider<TKey, TValue>
    {
        private readonly object _locker = new();
        private readonly ProducerConfig _config;
        private readonly ILogger<ProducerProvider<TKey, TValue>> _logger;
        private IProducer<TKey, TValue> _producer;
        private bool _disposedValue;

        public ProducerProvider(
            IConfiguration configuration,
            ILogger<ProducerProvider<TKey, TValue>> logger)
        {
            _config = configuration.GetSection("ProducerConfig").Get<ProducerConfig>()
                ?? throw new InfraKafkaException("There is no producer configuration data.");
            _logger = logger;
        }

        public IProducer<TKey, TValue> GetProducer() =>
            _producer ??= BuildProducer();

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _producer.Dispose();
                }

                _disposedValue = true;
            }
        }

        private IProducer<TKey, TValue> BuildProducer()
        {
            lock (_locker)
            {
                if (_producer is not null)
                {
                    return _producer;
                }

                _producer = new ProducerBuilder<TKey, TValue>(_config)
                    .SetErrorHandler((_, error) =>
                        _logger.LogError($"A {error.GetOrigin()} occurs: {error.Reason}"))
                    .SetLogHandler((_, logMessage) =>
                        _logger.LogInformation(logMessage.Message))
                    .Build();
            }

            return _producer;
        }
    }
}
