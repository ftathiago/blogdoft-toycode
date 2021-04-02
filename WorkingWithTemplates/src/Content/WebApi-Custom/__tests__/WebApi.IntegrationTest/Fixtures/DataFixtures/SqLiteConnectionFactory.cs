using System.Data;
using WebApi.InfraData.Contexts;

namespace WebApi.IntegrationTest.Fixtures.DataFixtures
{
    public class SqLiteConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection GetNewConnection() =>
            new SqLiteDatabase().Connection;
    }
}