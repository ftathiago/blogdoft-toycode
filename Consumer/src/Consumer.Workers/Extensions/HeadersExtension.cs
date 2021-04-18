using Confluent.Kafka;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Consumer.InfraKafka.Extensions
{
    public static class HeadersExtension
    {
        public static IDictionary<string, string> ToDictionary(this Headers headers) =>
            headers
                .ToDictionary(
                    item => item.Key,
                    item => Encoding.UTF8.GetString(item.GetValueBytes()));
    }
}
