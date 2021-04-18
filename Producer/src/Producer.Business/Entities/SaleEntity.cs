using Producer.Business.Entities.Validations;
using Producer.Business.Exceptions;
using Producer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Producer.Business.Entities
{
    public class SaleEntity : EntityBase
    {
        private readonly List<SaleItemEntity> _items = new();

        public SaleEntity(
            Guid id,
            string customerIdentity,
            string number,
            DateTime date)
        {
            Id = id;
            CustomerIdentity = customerIdentity;
            Number = number;
            Date = date;
        }

        public Guid Id { get; init; }

        public string CustomerIdentity { get; set; }

        public string Number { get; init; }

        public DateTime Date { get; set; }

        public IEnumerable<SaleItemEntity> Items => _items;

        public decimal GetAmount() => _items
            .Sum(item => item.Quantity * item.Value);

        public SaleEntity AddItem(Product product, decimal quantity, decimal value)
        {
            var item = new SaleItemEntity(product, quantity, value);
            return AddItem(item);
        }

        public SaleEntity AddItem(SaleItemEntity item)
        {
            if (!item.IsValid())
            {
                throw new InvalidEntityException(
                    nameof(SaleItemEntity),
                    item.GetValidations());
            }

            _items.Add(item);

            return this;
        }

        protected override FluentValidation.Results.ValidationResult GetValidator() =>
            new SaleEntityValidation().Validate(this);
    }
}
