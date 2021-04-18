using Producer.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Producer.Api.Model.Response
{
    public class SaleResponse
    {
        public Guid Id { get; init; }

        public string CustomerIdentity { get; set; }

        public string Number { get; init; }

        public DateTime Date { get; set; }

        public IEnumerable<SaleItemResponse> Items { get; set; }

        public static SaleResponse From(SaleEntity entity) => entity is null ?
        null : new()
        {
            Id = entity.Id,
            CustomerIdentity = entity.CustomerIdentity,
            Number = entity.Number,
            Date = entity.Date,
            Items = entity.Items.Select(SaleItemResponse.From),
        };
    }
}
