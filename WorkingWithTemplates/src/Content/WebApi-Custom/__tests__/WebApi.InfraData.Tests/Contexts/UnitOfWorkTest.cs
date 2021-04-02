using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Data;
using WebApi.InfraData.Contexts;
using WebApi.InfraData.Tests.Fixtures;
using WebApi.Shared.Exceptions;
using Xunit;

namespace WebApi.InfraData.Tests.Contexts
{
    public class UnitOfWorkTest : IDisposable
    {
        private readonly Mock<ILogger<UnitOfWork>> _logger;
        private readonly Mock<IDbTransaction> _transaction;
        private readonly Mock<IDbConnectionFactory> _connectionFactory;
        private readonly Mock<IDbConnection> _connection;

        public UnitOfWorkTest()
        {
            _connectionFactory = new Mock<IDbConnectionFactory>(MockBehavior.Strict);
            _connection = new Mock<IDbConnection>(MockBehavior.Strict);
            _logger = new Mock<ILogger<UnitOfWork>>(MockBehavior.Loose);
            _transaction = new Mock<IDbTransaction>(MockBehavior.Strict);
        }

        public void Dispose()
        {
            _connectionFactory.VerifyAll();
            _connection.VerifyAll();
            _logger.VerifyAll();
            _transaction.VerifyAll();
        }

        [Fact]
        public void ShouldBeNullWhenThereIsNoTransactionOpen()
        {
            // Given
            var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);

            // When
            var transaction = unitOfWork.Transaction;

            // Then
            transaction.Should().BeNull();
        }

        [Fact]
        public void ShouldNotBeNullWhenThereIsTransactionOpen()
        {
            // Given
            _connection.Setup(c => c.Open());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);
            unitOfWork.BeginTransaction();

            // When
            var transaction = unitOfWork.Transaction;

            // Then
            transaction.Should().NotBeNull();
        }

        [Fact]
        public void ShouldCreatePseudoNestedTransactions()
        {
            // Given
            _connection.Setup(c => c.Open());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);

            // When
            unitOfWork.BeginTransaction();
            var transaction1 = unitOfWork.Transaction;
            unitOfWork.BeginTransaction();
            var transaction2 = unitOfWork.Transaction;

            // Then
            transaction1.Should().Be(transaction2);
        }

        [Fact]
        public void ShouldSetTransactionToNullWhenCommit()
        {
            // Given
            _transaction.Setup(t => t.Commit());
            _transaction.Setup(t => t.Dispose());
            _connection.Setup(c => c.Open());
            _connection.Setup(c => c.Close());
            _connection.Setup(c => c.Dispose());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            using var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);

            // When
            unitOfWork.BeginTransaction();
            unitOfWork.Commit();

            // Then
            unitOfWork.Transaction.Should().BeNull();
        }

        [Fact]
        public void ShouldNotActualyCommitTransactionUntilThereIsNestedTransactionsOpened()
        {
            // Given
            _connection.Setup(c => c.Dispose());
            _connection.Setup(c => c.Open());
            _connection.Setup(c => c.Close());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            using var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);
            unitOfWork.BeginTransaction();
            unitOfWork.BeginTransaction();

            // When
            unitOfWork.Commit();

            // Then
            unitOfWork.Transaction.Should().NotBeNull();
            _connection.Verify(c => c.BeginTransaction(), Times.Once);
            _transaction.Verify(t => t.Commit(), Times.Never);
        }

        [Fact]
        public void ShouldClearTransactionWhenCallRollback()
        {
            // Given
            _transaction.Setup(t => t.Dispose());
            _transaction.Setup(t => t.Rollback());
            _connection.Setup(c => c.Open());
            _connection.Setup(c => c.Close());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);
            unitOfWork.BeginTransaction();

            // When
            unitOfWork.Rollback();

            // Then
            unitOfWork.Transaction.Should().BeNull();
        }

        [Fact]
        public void ShouldClearTransactionWhenCallRollbackEvenWhenHaveNestedTransactions()
        {
            // Given
            _transaction.Setup(t => t.Dispose());
            _transaction.Setup(t => t.Rollback());
            _connection.Setup(c => c.Open());
            _connection.Setup(c => c.Close());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);
            unitOfWork.BeginTransaction();
            unitOfWork.BeginTransaction();

            // When
            unitOfWork.Rollback();

            // Then
            unitOfWork.Transaction.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowExceptionWhenTryCommitANotOpenedTransaction()
        {
            // Given
            using var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);

            // When
            Action act = () => unitOfWork.Commit();

            // Then
            act.Should().Throw<NotOpenTransactionException>();
        }

        [Fact]
        public void ShouldThrowExceptionWhenTryRollbackANotOpenedTransaction()
        {
            // Given
            using var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);

            // When
            Action act = () => unitOfWork.Commit();

            // Then
            act.Should().Throw<NotOpenTransactionException>();
        }

        [Fact]
        public void ShouldReThrowExceptionWhenCommitThrowException()
        {
            // Given
            _transaction
                .Setup(t => t.Commit())
                .Throws(new MockedException());
            _transaction.Setup(t => t.Rollback());
            _transaction.Setup(t => t.Dispose());
            _connection.Setup(c => c.Open());
            _connection.Setup(c => c.Close());
            _connection.Setup(c => c.Dispose());
            _connection
                .Setup(c => c.BeginTransaction())
                .Returns(_transaction.Object);
            _connectionFactory
                .Setup(cf => cf.GetNewConnection())
                .Returns(_connection.Object);
            using var unitOfWork = new UnitOfWork(
                _logger.Object,
                _connectionFactory.Object);

            // When
            unitOfWork.BeginTransaction();
            Action act = () => unitOfWork.Commit();

            // Then
            act.Should().ThrowExactly<MockedException>();
        }
    }
}
