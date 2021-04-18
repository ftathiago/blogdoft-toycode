using System;
using Producer.Business.Entities;

namespace Producer.Api.Model.Request
{
    public class SaleItemRequest
    {
        public Guid ProductId { get; init; }

        public decimal Quantity { get; init; }

        public decimal Value { get; init; }
    }
}
