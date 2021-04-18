using System;

namespace Consumer.Business.Model
{
    public class ProductEvent
    {
        public Guid Id { get; }

        public string Description { get; }

        public decimal Value { get; }
    }
}
