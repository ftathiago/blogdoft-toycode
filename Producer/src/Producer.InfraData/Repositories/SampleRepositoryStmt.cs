using System.Security;

namespace Producer.InfraData.Repositories
{
    public static class SampleRepositoryStmt
    {
        public const string Insert = @"
            INSERT INTO Sales(
                id,
                document_id,
                sale_number,
                sale_date
            ) VALUES(
                @id,
                @document_id,
                @sale_number,
                @sale_date)";

        public const string InsertItem = @"
            INSERT INTO SaleItems (
                id,
                sale_id,
                product_id,
                value,
                quantity
            ) VALUES(
                @id,
                @sale_id,
                @product_id,
                @value,
                @quantity)";

        public const string Update = @"
            update SAMPLE_TABLE set 
                testproperty = @TestProperty, 
                active = @Active 
            where id = @id";

        public const string Remove = @"
            delete from SAMPLE_TABLE 
            where id = @id";

        public const string GetById = @"
            SELECT
                s.id,
                s.document_id,
                s.sale_number,
                s.sale_date,
                si.id as ItemId,
                si.product_id as ProductId,
                si.value as ItemValue,
                si.quantity as ItemQuantity,
                p.description as ProductDescription
            FROM
                Sales s
            JOIN SaleItems si ON
                si.sale_id = s.id
            JOIN Products p ON
                P.id = si.product_id
            WHERE
                s.id = @id";
    }
}
