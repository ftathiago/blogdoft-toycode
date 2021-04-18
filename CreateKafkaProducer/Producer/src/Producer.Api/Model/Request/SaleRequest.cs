using Producer.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Producer.Api.Model.Request
{
    public class SaleRequest
    {
        public SaleRequest()
        {
            Items = Array.Empty<SaleItemRequest>();
        }

        public Guid Id { get; init; }

        public string CustomerIdentity { get; set; }

        public string Number { get; init; }

        public DateTime Date { get; set; }

        public IEnumerable<SaleItemRequest> Items { get; set; }
    }
}
