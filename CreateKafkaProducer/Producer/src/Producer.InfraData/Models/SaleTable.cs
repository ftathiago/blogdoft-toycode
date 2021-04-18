using System;
using System.Collections.Generic;

namespace Producer.InfraData.Models
{
    public class SaleTable
    {
        public Guid Id { get; set; }

        public string Document_Id { get; set; }

        public string Sale_Number { get; set; }

        public DateTime Sale_Date { get; set; }

        public List<SaleItemTable> Items { get; set; } = new();
    }
}
