using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Producer.InfraData.Contexts
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _config;
        private readonly ILogger<DbConnectionFactory> _logger;

        public DbConnectionFactory(IConfiguration configuration, ILogger<DbConnectionFactory> logger) =>
            (_config, _logger) = (configuration.GetConnectionString("Default"), logger);

        public IDbConnection GetNewConnection()
        {
            try
            {
                return new SqlConnection(_config);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error creating connection: {0} - {1}", _config, exception.Message);
                throw;
            }
        }
    }
}
