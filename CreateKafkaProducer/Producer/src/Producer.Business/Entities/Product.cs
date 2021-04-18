using System;

namespace Producer.Business.Entities
{
    public class Product
    {
        public Product(
            Guid id,
            string description,
            decimal value)
        {
            Id = id;
            Description = description;
            Value = value;
        }

        public Guid Id { get; }

        public string Description { get; }

        public decimal Value { get; }
    }
}
