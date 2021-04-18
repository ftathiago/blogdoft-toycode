using Confluent.Kafka;
using System;

namespace Consumer.Workers.Providers
{
    internal interface IConsumerProvider<TKey, TValue> : IDisposable
    {
        IConsumer<TKey, TValue> GetConsumer();
    }
}
