using Producer.Business.Entities;
using System;

namespace Producer.Api.Model.Response
{
    public class SaleItemResponse
    {
        public Guid ProductId { get; init; }

        public string Description { get; init; }

        public decimal Quantity { get; init; }

        public decimal Value { get; init; }

        public static SaleItemResponse From(SaleItemEntity item) => item is null
        ? null : new()
        {
            ProductId = item.Product.Id,
            Description = item.Product.Description,
            Quantity = item.Quantity,
            Value = item.Value,
        };
    }
}
