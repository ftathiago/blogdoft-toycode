using System;
using System.Runtime.Serialization;

namespace Producer.InfraKafka.Exceptions
{
    [Serializable]
    public class InfraKafkaException : Exception
    {
        public InfraKafkaException()
        {
        }

        public InfraKafkaException(string message)
            : base(message)
        {
        }

        public InfraKafkaException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected InfraKafkaException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
