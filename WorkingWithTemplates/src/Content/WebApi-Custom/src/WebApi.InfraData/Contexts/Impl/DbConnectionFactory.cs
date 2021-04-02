using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApi.InfraData.Contexts.Impl
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _config;

        public DbConnectionFactory(IConfiguration configuration) =>
            _config = configuration.GetConnectionString("Default");

        public IDbConnection GetNewConnection() => new SqlConnection(_config);
    }
}