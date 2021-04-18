namespace Consumer.Business.Model
{
    public class SaleItemEvent
    {
        public ProductEvent Product { get; init; }

        public decimal Quantity { get; init; }

        public decimal Value { get; init; }

        public decimal Total => Value * Quantity;
    }
}
