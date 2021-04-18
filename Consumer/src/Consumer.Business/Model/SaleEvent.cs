using System;
using System.Collections.Generic;

namespace Consumer.Business.Model
{
    public class SaleEvent
    {
        private readonly List<SaleItemEvent> _items = new();

        public Guid Id { get; set; }

        public string CustomerIdentity { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<SaleItemEvent> Items => _items;
    }
}
