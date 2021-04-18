using Confluent.Kafka;
using System;

namespace Consumer.Workers.Configurations
{
    public class ConsumerConfiguration : ConsumerConfig
    {
        private const int DefaultProcessingTimeoutMinutes = 1;
        private const int DefaultHeartbeatTimeoutSeconds = 10;

        public ConsumerConfiguration()
        {
            MaxPollIntervalMs = (int)TimeSpan.FromMinutes(DefaultProcessingTimeoutMinutes).TotalMilliseconds;
            SessionTimeoutMs = (int)TimeSpan.FromSeconds(DefaultHeartbeatTimeoutSeconds).TotalMilliseconds;
            GroupId = Guid.NewGuid().ToString();
        }

        public string Topic { get; init; }
    }
}
