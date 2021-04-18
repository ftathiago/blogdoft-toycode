using Confluent.Kafka;

namespace Producer.InfraKafka.Extensions
{
    public static class KafkaErrorExtension
    {
        public static string GetOrigin(this Error error)
        {
            if (error.IsBrokerError)
            {
                return "Broker";
            }

            return "Local";
        }
    }
}
