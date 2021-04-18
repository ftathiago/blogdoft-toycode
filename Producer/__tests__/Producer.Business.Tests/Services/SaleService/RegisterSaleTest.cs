using FluentAssertions;
using Moq;
using Producer.Business.Entities;
using Producer.Business.Exceptions;
using Producer.Business.Tests.Fixtures;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Producer.Business.Tests.Services.SaleService
{
    public class RegisterSaleTest : SaleServiceTestBase
    {
        [Fact]
        public async Task ShouldRegisterANewSale()
        {
            // Given
            var entity = SaleEntityFixture.BuildValidSaleEntity();
            Producer
                .Setup(p => p.PublishAsync(entity))
                .Returns(Task.CompletedTask);
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Commit());
            Repository
                .Setup(r => r.AddAsync(entity))
                .Returns(Task.CompletedTask);
            var service = BuildSaleService();

            // When
            var registeredEntity = await service.RegisterSale(entity);

            // Then
            Repository.VerifyAll();
            UnitOfWork.VerifyAll();
            registeredEntity.Should().Be(entity);
        }

        [Fact]
        public async Task ShouldProduceAMessageWhenCommitSuccessfully()
        {
            // Given
            var entity = SaleEntityFixture.BuildValidSaleEntity();
            Producer
                .Setup(p => p.PublishAsync(entity))
                .Returns(Task.CompletedTask);
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Commit());
            Repository
                .Setup(r => r.AddAsync(entity))
                .Returns(Task.CompletedTask);
            var service = BuildSaleService();

            // When
            var registeredEntity = await service.RegisterSale(entity);

            // Then
            Producer.VerifyAll();
            registeredEntity.Should().Be(entity);
        }

        [Fact]
        public async Task ShouldCallRollbackWhenRepositoryCallsException()
        {
            // Given
            var entity = SaleEntityFixture.BuildValidSaleEntity();
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork.Setup(uow => uow.Rollback());
            Repository
                .Setup(r => r.AddAsync(entity))
                .ThrowsAsync(new FixtureException());
            var service = BuildSaleService();

            // When
            Func<Task<SaleEntity>> action = async () => await service.RegisterSale(entity);

            // Then
            await action.Should().ThrowAsync<FixtureException>();
            Repository.VerifyAll();
            UnitOfWork.VerifyAll();
        }

        [Fact]
        public async Task ShouldCallRollbackWhenCommitCallsException()
        {
            // Given
            var entity = SaleEntityFixture.BuildValidSaleEntity();
            UnitOfWork.Setup(uow => uow.BeginTransaction());
            UnitOfWork
                .Setup(uow => uow.Commit())
                .Throws<FixtureException>();
            UnitOfWork.Setup(uow => uow.Rollback());
            Repository
                .Setup(r => r.AddAsync(entity))
                .Returns(Task.CompletedTask);
            var service = BuildSaleService();

            // When
            Func<Task<SaleEntity>> action = async () => await service.RegisterSale(entity);

            // Then
            await action.Should().ThrowExactlyAsync<FixtureException>();
            Repository.VerifyAll();
            UnitOfWork.VerifyAll();
        }

        [Fact]
        public async Task ShouldThrowsExceptionWhenSaleEntityIsInvalid()
        {
            // Given
            var sale = new SaleEntity(Guid.Empty, string.Empty, string.Empty, DateTime.MinValue);
            UnitOfWork.Setup(u => u.BeginTransaction());
            UnitOfWork.Setup(u => u.Rollback());
            Repository.Setup(r => r.AddAsync(sale)).Returns(Task.CompletedTask);
            var service = BuildSaleService();

            // When
            Func<Task<SaleEntity>> act = async () => await service.RegisterSale(sale);

            // Then
            await act.Should().ThrowAsync<InvalidEntityException>();
        }
    }
}
