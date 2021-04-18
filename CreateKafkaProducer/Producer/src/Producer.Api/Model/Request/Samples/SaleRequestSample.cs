using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Producer.Api.Model.Request.Samples
{
    public class SaleRequestSample : IExamplesProvider<SaleRequest>
    {
        public SaleRequest GetExamples() => new()
        {
            Id = Guid.NewGuid(),
            Number = "1234567",
            Date = DateTime.Now,
            CustomerIdentity = "00000000000",
            Items = new List<SaleItemRequest>
            {
                {
                    new SaleItemRequest()
                    {
                        ProductId = Guid.Parse("E8620544-17B9-4CB9-A9E8-765163B40530"),
                        Quantity = 1,
                        Value = 10,
                    }
                },
            },
        };
    }
}
