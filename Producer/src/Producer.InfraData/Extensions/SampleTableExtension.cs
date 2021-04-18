using Producer.Business.Entities;
using Producer.InfraData.Models;
using System.Linq;

namespace Producer.InfraData.Extensions
{
    public static class SampleTableExtension
    {
        public static SaleEntity AsEntity(this SaleTable table) => table?.Items
            .Select(item => item.AsEntity())
            .Aggregate(
                new SaleEntity(
                    table.Id,
                    table.Document_Id,
                    table.Sale_Number,
                    table.Sale_Date),
                (sale, item) => sale.AddItem(item));

        public static SaleItemEntity AsEntity(this SaleItemTable table) =>
            table is null
                ? default
                : new SaleItemEntity(
                    product: new Product(table.Product_Id, table.ProductDescription, table.Value),
                    quantity: table.Quantity,
                    value: table.Value);
    }
}
