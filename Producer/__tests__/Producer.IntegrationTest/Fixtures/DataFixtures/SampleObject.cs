using Producer.InfraData.Models;
using System.Collections.Generic;

namespace Producer.IntegrationTest.Fixtures.DataFixtures
{
    public static class SampleObject
    {
        private static IEnumerable<SampleTable> _samples;

        public static IEnumerable<SampleTable> SampleTables() =>
            _samples ??= new List<SampleTable>()
            {
                CreateSampleTable(),
                CreateSampleTable(),
            };

        private static SampleTable CreateSampleTable() => new()
        {
            Id = Fixture.Get().Random.Int(),
            TestProperty = Fixture.Get().Lorem.Sentence(),
            Active = Fixture.Get().Random.Bool(),
        };
    }
}
