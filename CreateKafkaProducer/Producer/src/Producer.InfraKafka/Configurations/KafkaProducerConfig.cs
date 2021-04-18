namespace Producer.InfraKafka.Configurations
{
    public class KafkaProducerConfig
    {
        public string BootstrapServers { get; set; }

        public string Topic { get; set; }
    }
}
