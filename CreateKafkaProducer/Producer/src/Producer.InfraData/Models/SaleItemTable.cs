using System;

namespace Producer.InfraData.Models
{
    public class SaleItemTable
    {
        public Guid Id { get; set; }

        public Guid Product_Id { get; set; }

        public decimal Value { get; set; }

        public decimal Quantity { get; set; }

        public string ProductDescription { get; set; }

        public static SaleItemTable From(SaleItemSelect item) => new()
        {
            Id = item.Item_Id,
            Product_Id = item.ProductId,
            ProductDescription = item.ProductDescription,
            Value = item.ItemValue,
            Quantity = item.ItemQuantity,
        };
    }
}
