using WebApi.Business.Entities;

namespace WebApi.Api.Model.Response
{
    public class SampleResponse
    {
        public int Id { get; set; }

        public string TestProperty { get; set; }

        public bool Active { get; set; }

        public static SampleResponse From(SampleEntity sample) => new()
        {
            Id = sample.Id,
            TestProperty = sample.TestProperty,
            Active = sample.Active,
        };
    }
}