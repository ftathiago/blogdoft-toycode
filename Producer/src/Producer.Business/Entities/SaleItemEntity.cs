using FluentValidation.Results;
using Producer.Business.Entities.Validations;
using Producer.Shared.Entities;
using System;

namespace Producer.Business.Entities
{
    public class SaleItemEntity : EntityBase
    {
        public SaleItemEntity(Product product, decimal quantity, decimal value)
        {
            Product = product;
            Quantity = quantity;
            Value = value;
        }

        public Product Product { get; init; }

        public decimal Quantity { get; init; }

        public decimal Value { get; init; }

        public decimal Total => Value * Quantity;

        protected override ValidationResult GetValidator() =>
            new SaleItemEntityValidator().Validate(this);
    }
}
