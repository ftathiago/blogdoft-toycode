using System;

namespace Producer.InfraData.Models
{
    public class SaleItemSelect
    {
        public Guid Item_Id { get; set; }

        public Guid ProductId { get; set; }

        public decimal ItemValue { get; set; }

        public decimal ItemQuantity { get; set; }

        public string ProductDescription { get; set; }
    }
}
