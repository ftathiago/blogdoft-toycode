using Confluent.Kafka;
using Consumer.Business.Model;
using System;
using System.Text.Json;

namespace Consumer.Workers.Serializers
{
    public class SaleEventDeserializer : IDeserializer<SaleEvent>
    {
        public SaleEvent Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            if (isNull)
            {
                return null;
            }

            return JsonSerializer.Deserialize<SaleEvent>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}
