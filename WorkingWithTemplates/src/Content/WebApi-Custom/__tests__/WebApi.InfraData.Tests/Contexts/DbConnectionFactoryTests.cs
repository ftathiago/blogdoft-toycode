using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Data.SqlClient;
using WebApi.InfraData.Contexts.Impl;
using Xunit;

namespace WebApi.InfraData.Tests.Contexts
{
    public class DbConnectionFactoryTests
    {
        private const string ConnectionStringTest =
            "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

        private readonly Mock<IConfiguration> _configuration;

        public DbConnectionFactoryTests() =>
            _configuration = new Mock<IConfiguration>(MockBehavior.Strict);

        [Fact]
        public void ShouldReturnASqlConnection()
        {
            // Given
            var defaultSection = new Mock<IConfigurationSection>();
            defaultSection
                .SetupGet(m => m[It.Is<string>(s => s == "Default")])
                .Returns(ConnectionStringTest);
            _configuration
                .Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings")))
                .Returns(defaultSection.Object);

            var factory = new DbConnectionFactory(_configuration.Object);

            // When
            var conn = factory.GetNewConnection() as SqlConnection;

            // Then
            conn.Should().NotBeNull();
        }
    }
}