using Producer.InfraData.Contexts;
using System.Data;

namespace Producer.IntegrationTest.Fixtures.DataFixtures
{
    public class SqLiteConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection GetNewConnection() =>
            new SqLiteDatabase().Connection;
    }
}
