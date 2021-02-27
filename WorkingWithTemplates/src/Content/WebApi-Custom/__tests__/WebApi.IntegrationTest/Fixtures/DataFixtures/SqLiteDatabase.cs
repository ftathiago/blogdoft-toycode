using System.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;

namespace WebApi.IntegrationTest.Fixtures.DataFixtures
{
    public class SqLiteDatabase
    {
        private readonly OrmLiteConnectionFactory dbFactory =
            new(":memory:", SqliteOrmLiteDialectProvider.Instance);

        public SqLiteDatabase()
        {
            Connection = this.dbFactory.OpenDbConnection();
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