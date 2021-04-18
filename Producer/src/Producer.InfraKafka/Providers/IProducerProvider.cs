using System;
using Confluent.Kafka;

namespace Producer.InfraKafka.Providers
{
    public interface IProducerProvider<TKey, TValue> : IDisposable
    {
        IProducer<TKey, TValue> GetProducer();
    }
}
