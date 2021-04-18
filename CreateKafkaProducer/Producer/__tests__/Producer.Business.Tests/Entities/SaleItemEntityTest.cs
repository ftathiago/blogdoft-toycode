using Bogus;
using FluentAssertions;
using Producer.Business.Entities;
using Producer.Business.Tests.Fixtures;
using System;
using Xunit;

namespace Producer.Business.Tests.Entities
{
    public class SaleItemEntityTest
    {
        private const decimal MinimalValueRange = 0.01M;
        private readonly Faker _faker = Fixture.Get();

        [Fact]
        public void ShouldCreateAValidItem()
        {
            // Given
            var item = SaleEntityFixture.BuildSaleItemEntity();

            // When
            var isValid = item.IsValid();

            // Then
            isValid.Should().BeTrue();
        }

        [Fact]
        public void ShouldSaleItemBeInvalidWhenQuantityIsLessOrEqualThanZero()
        {
            // Given
            var item = new SaleItemEntity(
                SaleEntityFixture.GetProduct(),
                0,
                1);

            // When
            var isValid = item.IsValid();
            var validations = item.GetValidations();

            // Then
            isValid.Should().BeFalse();
            validations.Should().Contain(error =>
                error.ErrorMessage.Equals("Quantity must be greather than zero."));
        }

        [Fact]
        public void ShouldSaleItemBeInvalidWhenValueIsLessOrEqualThanZero()
        {
            // Given
            var item = new SaleItemEntity(
                SaleEntityFixture.GetProduct(),
                1,
                0);

            // When
            var isValid = item.IsValid();
            var validations = item.GetValidations();

            // Then
            isValid.Should().BeFalse();
            validations.Should().Contain(error =>
                error.ErrorMessage.Equals("Value must be greather than zero."));
        }
    }
}
