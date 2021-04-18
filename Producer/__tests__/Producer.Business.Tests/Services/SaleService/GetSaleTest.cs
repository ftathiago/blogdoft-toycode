using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using Producer.Business.Entities;
using Producer.Business.Tests.Fixtures;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Producer.Business.Tests.Services
{
    public class GetSaleTest : SaleServiceTestBase
    {
        [Fact]
        public async Task ShouldReturnSampleEntityWhenFoundIt()
        {
            // Given
            var expectedEntity = GetSampleEntity();
            Repository
                .Setup(s => s.GetByIdAsync(expectedEntity.Id))
                .ReturnsAsync(expectedEntity);
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Commit());
            Producer
                .Setup(p => p.PublishAsync(expectedEntity))
                .Returns(Task.CompletedTask);
            var service = BuildSaleService();

            // When
            var entity = await service.GetSaleAsync(expectedEntity.Id);

            // Then
            entity.Should().NotBeNull();
            entity.Should().Be(expectedEntity);
        }

        [Fact]
        public async Task ShouldReturnNullWhenNotFoundId()
        {
            // Given
            Repository
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(null as SaleEntity);
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Commit());
            var service = BuildSaleService();

            // When
            var entity = await service.GetSaleAsync(Guid.NewGuid());

            // Then
            entity.Should().BeNull();
        }

        [Fact]
        public async Task ShouldNotThrowExceptions()
        {
            var exceptionThrowed = new TestException();
            const HttpStatusCode ExpectedStatusCode = HttpStatusCode.InternalServerError;
            MessageHolder
                .Setup(mh => mh.AddMessage(
                    ExpectedStatusCode,
                    exceptionThrowed.Message));
            Repository
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Throws(exceptionThrowed);
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Rollback());
            var service = BuildSaleService();

            // When
            Func<Task> act = async () => await service.GetSaleAsync(Guid.NewGuid());

            // Then
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task ShouldRollbackWhenAnyErrorOccursInsideRepository()
        {
            var exceptionThrowed = new TestException();
            const HttpStatusCode ExpectedStatusCode = HttpStatusCode.InternalServerError;
            MessageHolder
                .Setup(mh => mh.AddMessage(
                    ExpectedStatusCode,
                    exceptionThrowed.Message));
            Repository
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Throws(exceptionThrowed);
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Rollback());
            var service = BuildSaleService();

            // When
            var entity = await service.GetSaleAsync(Guid.NewGuid());

            // Then
            entity.Should().BeNull();
        }

        private SaleEntity GetSampleEntity() => new(
            id: Guid.NewGuid(),
            customerIdentity: Faker.Person.Cpf(),
            number: Faker.Random.Int().ToString(),
            date: DateTime.Now);
    }
}
