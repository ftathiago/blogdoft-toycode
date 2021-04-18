using Bogus;

namespace Producer.InfraData.Tests.Fixtures
{
    public static class Fixture
    {
        private static Faker _faker;

        public static Faker Get() => _faker ??= new("pt_BR");

        public static Faker<TObject> Get<TObject>()
            where TObject : class => new("pt_BR");
    }
}
