using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Sqlite;
using System.Data;

namespace Producer.InfraData.Tests.Fixtures.DataFixtures
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
            foreach (var table in DatabaseScripts.CreateDatabase())
            {
                connection.ExecuteSql(table);
            }

            // connection.ExecuteSql(DatabaseScripts.SeedSampleTable);
        }
    }
}
