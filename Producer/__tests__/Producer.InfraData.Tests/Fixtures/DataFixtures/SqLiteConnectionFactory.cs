using Producer.InfraData.Contexts;
using System.Data;

namespace Producer.InfraData.Tests.Fixtures.DataFixtures
{
    public class SqLiteConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection GetNewConnection() =>
            new SqLiteDatabase().Connection;
    }
}
