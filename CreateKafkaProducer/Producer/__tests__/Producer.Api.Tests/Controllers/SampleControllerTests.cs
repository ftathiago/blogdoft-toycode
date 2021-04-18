using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Producer.Api.Controllers;
using Producer.Api.Tests.Fixtures;
using Producer.Business.Entities;
using Producer.Business.Repositories;
using Producer.Business.Services;
using Producer.Shared.Extensions;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Producer.Api.Tests.Controllers
{
    public class SampleControllerTests
    {
        private readonly Faker _faker = Fixture.Get();
        private readonly Mock<IProductRepository> _repository;

        public SampleControllerTests()
        {
            _repository = new Mock<IProductRepository>();
        }

        [Fact]
        public async Task ShouldReturnOkResultWithSampleResponse()
        {
            // Given
            var mockSampleTable = GetSampleEntity();
            var service = new Mock<ISaleService>(MockBehavior.Strict);
            service
                .Setup(s => s.GetSaleAsync(mockSampleTable.Id))
                .ReturnsAsync(mockSampleTable);
            var controller = new SalesController(
                service.Object,
                _repository.Object);

            // When
            var response = await controller.GetAsync(mockSampleTable.Id);
            var result = response as OkObjectResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK.AsInteger());
        }

        [Fact]
        public async Task ShouldReturnBadRequestWhenIdIsNull()
        {
            // Given
            var service = new Mock<ISaleService>(MockBehavior.Strict);
            var controller = new SalesController(
                service.Object,
                _repository.Object);

            // When
            var response = await controller.GetAsync(null);
            var result = response as BadRequestResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest.AsInteger());
        }

        [Fact]
        public async Task ShouldReturnNotFoundWhenSampleDoesNotExists()
        {
            // Given
            var notExistId = Guid.NewGuid();
            var service = new Mock<ISaleService>(MockBehavior.Strict);
            service
                .Setup(s => s.GetSaleAsync(notExistId))
                .ReturnsAsync(null as SaleEntity);
            var controller = new SalesController(
                service.Object,
                _repository.Object);

            // When
            var response = await controller.GetAsync(notExistId);
            var result = response as NotFoundResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound.AsInteger());
        }

        private SaleEntity GetSampleEntity() => new(
            id: Guid.NewGuid(),
            customerIdentity: _faker.Person.Cpf(),
            number: _faker.Random.Int().ToString(),
            date: DateTime.Now);
    }
}
