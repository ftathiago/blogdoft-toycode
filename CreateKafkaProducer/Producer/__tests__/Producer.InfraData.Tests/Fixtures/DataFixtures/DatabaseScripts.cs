using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Producer.InfraData.Tests.Fixtures.DataFixtures
{
    public static class DatabaseScripts
    {
        public static IEnumerable<string> CreateDatabase()
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
    }
}
