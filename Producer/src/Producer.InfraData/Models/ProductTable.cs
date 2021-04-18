using System;

namespace Producer.InfraData.Models
{
    public class ProductTable
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public decimal Value { get; set; }

        public bool Active { get; set; }
    }
}
