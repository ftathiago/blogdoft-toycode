using System.Linq;
using System.Text;

namespace WebApi.IntegrationTest.Fixtures.DataFixtures
{
    public static class DatabaseScripts
    {
        public static readonly string CREATE_SALES_REPORT_DATABASE =
#if (!excludeSamples)        
            @"
                CREATE TABLE sample_table (
                    id INTEGER PRIMARY KEY NOT NULL,
                    testproperty varchar(50) NOT NULL,
                    active INTEGER NOT NULL
                );
            ";
#endif
#if (excludeSamples)
            "Your table definitions script goes here";
#endif

        public static readonly string SeedSampleTable =
#if (!excludeSamples)
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
#endif
#if (excludeSamples)
            "Your seed script goes here";
#endif
    }
}