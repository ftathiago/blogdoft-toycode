using Bogus;
using Bogus.Extensions.Brazil;
using Producer.Business.Entities;
using System;
using System.Text;

namespace Producer.Business.Tests.Fixtures
{
    public static class SaleEntityFixture
    {
        private static readonly Faker _faker = Fixture.Get();

        public static SaleEntity BuildSaleEntity() => new(
            id: Guid.NewGuid(),
            customerIdentity: _faker.Person.Cpf(),
            number: BuildSaleNumber(),
            date: DateTime.Now);

        public static SaleItemEntity BuildSaleItemEntity() => new(
            product: GetProduct(),
            quantity: _faker.Random.Decimal(0.01M),
            value: _faker.Random.Decimal(0.01M));

        public static SaleEntity BuildValidSaleEntity()
        {
            var sale = BuildSaleEntity();
            sale.AddItem(
                product: GetProduct(),
                quantity: _faker.Random.Decimal(1, 2),
                value: _faker.Random.Decimal(1, 2));
            return sale;
        }

        public static string BuildSaleNumber() =>
            _faker.Random.Int(0, 999999).ToString("D7");

        public static SaleItemEntity GetSaleItem() => Fixture.Get<SaleItemEntity>()
            .RuleFor(s => s.Product, GetProduct())
            .RuleFor(s => s.Quantity, (fk, _) => fk.Random.Decimal(0.01M))
            .RuleFor(s => s.Value, (_, item) => item.Product.Value * item.Quantity);

        public static Product GetProduct() => new(
            id: Guid.NewGuid(),
            description: _faker.Lorem.Sentence(),
            value: _faker.Random.Decimal(0.01M, decimal.MaxValue));
    }
}
