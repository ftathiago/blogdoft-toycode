using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Producer.InfraData.Contexts;
using System.Data.SqlClient;
using Xunit;

namespace Producer.InfraData.Tests.Contexts
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
            var logger = new Mock<ILogger<DbConnectionFactory>>();

            var factory = new DbConnectionFactory(_configuration.Object, logger.Object);

            // When
            var conn = factory.GetNewConnection() as SqlConnection;

            // Then
            conn.Should().NotBeNull();
        }
    }
}
