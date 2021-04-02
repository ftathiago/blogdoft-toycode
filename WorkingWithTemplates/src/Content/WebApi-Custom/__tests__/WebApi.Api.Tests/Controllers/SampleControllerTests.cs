using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using WebApi.Api.Controllers;
using WebApi.Api.Model.Response;
using WebApi.Api.Tests.Fixtures;
using WebApi.Business.Entities;
using WebApi.Business.Services;
using WebApi.Shared.Extensions;
using Xunit;

namespace WebApi.Api.Tests.Controllers
{
    public class SampleControllerTests
    {
        [Fact]
        public void ShouldReturnOkResultWithSampleResponse()
        {
            // Given
            var mockSampleTable = GetSampleEntity();
            var expectedResponse = GetSampleResponse(mockSampleTable);
            var service = new Mock<ISampleService>(MockBehavior.Strict);
            service
                .Setup(s => s.GetSampleBy(mockSampleTable.Id))
                .Returns(mockSampleTable);
            var controller = new SampleController(service.Object);

            // When
            var response = controller.Get(mockSampleTable.Id);
            var result = response as OkObjectResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.OK.AsInteger());
        }

        [Fact]
        public void ShouldReturnBadRequestWhenIdIsNull()
        {
            // Given
            var service = new Mock<ISampleService>(MockBehavior.Strict);
            var controller = new SampleController(service.Object);

            // When
            var response = controller.Get(null);
            var result = response as BadRequestResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest.AsInteger());
        }

        [Fact]
        public void ShouldReturnNotFoundWhenSampleDoesNotExists()
        {
            // Given
            var notExistId = Fixture.Get().Random.Int();
            var service = new Mock<ISampleService>(MockBehavior.Strict);
            service
                .Setup(s => s.GetSampleBy(It.IsAny<int>()))
                .Returns(null as SampleEntity);
            var controller = new SampleController(service.Object);

            // When
            var response = controller.Get(notExistId);
            var result = response as NotFoundResult;

            // Then
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound.AsInteger());
        }

        private static SampleEntity GetSampleEntity() => new()
        {
            Id = Fixture.Get().Random.Int(),
            TestProperty = Fixture.Get().Lorem.Sentence(),
            Active = Fixture.Get().Random.Bool(),
        };

        private static SampleResponse GetSampleResponse(SampleEntity entity)
        {
            if (entity is not null)
            {
                return SampleResponse.From(entity);
            }

            return new SampleResponse
            {
                Id = Fixture.Get().Random.Int(),
                TestProperty = Fixture.Get().Lorem.Sentence(),
                Active = Fixture.Get().Random.Bool(),
            };
        }
    }
}