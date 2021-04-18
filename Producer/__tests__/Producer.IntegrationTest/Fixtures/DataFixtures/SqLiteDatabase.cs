using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System.Data;

namespace Producer.IntegrationTest.Fixtures.DataFixtures
{
    public class SqLiteDatabase
    {
        private readonly OrmLiteConnectionFactory _dbFactory =
            new(":memory:", SqliteOrmLiteDialectProvider.Instance);

        public SqLiteDatabase()
        {
            Connection = _dbFactory.OpenDbConnection();
            CreateDatabase();
        }

        public IDbConnection Connection { get; }

        private void CreateDatabase()
        {
            using var connection = this.Connection;
            connection.ExecuteSql(DatabaseScripts.CREATE_SALES_REPORT_DATABASE);
            connection.ExecuteSql(DatabaseScripts.SeedSampleTable);
        }
    }
}
