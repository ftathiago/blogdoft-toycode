using System.Linq;
using System.Text;

namespace Producer.IntegrationTest.Fixtures.DataFixtures
{
    public static class DatabaseScripts
    {
        public static readonly IEnumerable<string> CreateDatabase = () =>
            { 
                yield return @"
                    CREATE TABLE sample_table (
                        id INTEGER PRIMARY KEY NOT NULL,
                        testproperty varchar(50) NOT NULL,
                        active INTEGER NOT NULL
                    )";
                yield return @"
                    CREATE TABLE Sales (
                        id uniqueidentifier NOT NULL PRIMARY KEY,
                        document_id char(14) NOT NULL,
                        sale_number char(10) NOT NULL,
                        sale_date datetime NOT NULL,
                    )";
            }

        public static readonly string SeedSampleTable =
            SampleObject.SampleTables()
                .Aggregate(new StringBuilder(), (sb, obj) =>
                    sb
                        .Append("INSERT INTO sample_table (id, testproperty, active) VALUES (")
                        .Append(obj.Id)
                        .Append(", '")
                        .Append(obj.TestProperty)
                        .Append("', ")
                        .Append(obj.Active)
                        .AppendLine(");"))
                .ToString();
    }
}
