using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Producer.Business.Entities;
using Producer.Business.Exceptions;
using Producer.Business.Tests.Fixtures;
using System;
using Xunit;

namespace Producer.Business.Tests.Entities
{
    public class SaleEntityTest
    {
        private readonly Faker _faker = Fixture.Get();

        [Fact]
        public void ShouldCreateSaleEntity()
        {
            // Given
            var id = Guid.NewGuid();
            var customerIdentity = _faker.Person.Cpf();
            var number = _faker.Random.Int().ToString();
            var date = DateTime.Now;

            // When
            var sale = new SaleEntity(
                id,
                customerIdentity,
                number,
                date);

            // Then
            sale.Should().BeEquivalentTo(new
            {
                Id = id,
                CustomerIdentity = customerIdentity,
                Number = number,
                Date = date,
            });
        }

        [Fact]
        public void ShouldSaleBeValidWhenNumberHaveSevenDigits()
        {
            // Given
            var numberWithSevenDigits = SaleEntityFixture.BuildSaleNumber();
            var entity = new SaleEntity(
                id: Guid.NewGuid(),
                customerIdentity: _faker.Person.Cpf(),
                number: numberWithSevenDigits,
                date: DateTime.Now);
            entity.AddItem(SaleEntityFixture.BuildSaleItemEntity());

            // When 
            var isValid = entity.IsValid();

            // Then
            isValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldSaleBeInvalidWhenNumberHaveLessThanSevenDigits()
        {
            // Given
            var numberWithSevenDigits = _faker.Random.Int(0, 99999).ToString("D6");
            var entity = new SaleEntity(
                id: Guid.NewGuid(),
                customerIdentity: _faker.Person.Cpf(),
                number: numberWithSevenDigits,
                date: DateTime.Now);

            // When 
            var isValid = entity.IsValid();

            // Then
            isValid.Should().BeFalse();
            entity.GetValidations().Should().Contain(error =>
                error.ErrorMessage.Equals("Sale number should have seven digits."));
        }

        [Fact]
        public void ShouldSaleBeInvalidWhenNumberHaveAlphaChars()
        {
            // Given
            var numberWithSevenDigits = _faker.Lorem.Words(7).ToString().Substring(0, 7);
            var entity = new SaleEntity(
                id: Guid.NewGuid(),
                customerIdentity: _faker.Person.Cpf(),
                number: numberWithSevenDigits,
                date: DateTime.Now);

            // When 
            var isValid = entity.IsValid();
            var validations = entity.GetValidations();

            // Then
            isValid.Should().BeFalse();
            validations.Should().Contain(error =>
                error.ErrorMessage.Equals("Sale number should have seven digits."));
        }

        [Fact]
        public void ShouldSaleValidWhenCustomerIdentityIsAValidCpf()
        {
            // Given
            var customerIdentity = _faker.Person.Cpf();
            var entity = new SaleEntity(
                id: Guid.NewGuid(),
                customerIdentity: customerIdentity,
                number: SaleEntityFixture.BuildSaleNumber(),
                date: DateTime.Now);
            entity.AddItem(SaleEntityFixture.BuildSaleItemEntity());

            // When 
            var isValid = entity.IsValid();

            // Then
            isValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldSaleBeInvalidWhenCustomerIdentityIsAnInvalidCpf()
        {
            // Given
            const string CustomerIdentity = "0";
            var entity = new SaleEntity(
                id: Guid.NewGuid(),
                customerIdentity: CustomerIdentity,
                number: SaleEntityFixture.BuildSaleNumber(),
                date: DateTime.Now);

            // When 
            var isValid = entity.IsValid();

            // Then
            isValid.Should().BeFalse();
            entity.GetValidations().Should().Contain(error =>
                error.ErrorMessage.Equals("Customer identity should be a valid CPF."));
        }

        [Fact]
        public void ShouldSaleHaveAtLessOneItemToBeValid()
        {
            // Given
            var sale = SaleEntityFixture.BuildValidSaleEntity();

            // When
            var isValid = sale.IsValid();

            // Then
            isValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldSaleBeInvalidWhenHaveNoItens()
        {
            // Given
            var sale = SaleEntityFixture.BuildSaleEntity();

            // When
            var isValid = sale.IsValid();
            var validations = sale.GetValidations();

            // Then
            isValid.Should().BeFalse();
            validations.Should().Contain(error =>
                error.ErrorMessage.Equals("A sale should have one item at last."));
        }

        [Fact]
        public void ShouldAddAItemToSale()
        {
            // Given
            var product = SaleEntityFixture.GetProduct();
            var itemValue = _faker.Random.Decimal();
            var itemQuantity = _faker.Random.Decimal();
            var expectedTotal = itemValue * itemQuantity;
            var entity = SaleEntityFixture.BuildSaleEntity();

            // When
            entity.AddItem(product, itemQuantity, itemValue);

            // Then
            entity.GetAmount().Should().Be(expectedTotal);
            entity.Items.Should().OnlyContain(item =>
                item.Product.Id.Equals(product.Id) &&
                item.Value.Equals(itemValue) &&
                item.Quantity.Equals(itemQuantity));
            entity.Items.Should().HaveCount(1);
        }

        [Fact]
        public void ShouldThrowInvalidEntityExceptionWhenAddAnInvalidSaleItemEntity()
        {
            // Given
            const decimal ItemValue = 0M;
            const decimal ItemQuantity = 0M;
            var product = SaleEntityFixture.GetProduct();
            var entity = SaleEntityFixture.BuildSaleEntity();

            // When
            Action act = () => entity.AddItem(product, ItemQuantity, ItemValue);

            // Then
            act.Should().ThrowExactly<InvalidEntityException>();
        }
    }
}
