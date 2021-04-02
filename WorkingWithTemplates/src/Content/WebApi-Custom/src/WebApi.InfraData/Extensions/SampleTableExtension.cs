using WebApi.Business.Entities;
using WebApi.InfraData.Models;

namespace WebApi.InfraData.Extensions
{
    public static class SampleTableExtension
    {
        public static SampleEntity AsEntity(this SampleTable table) => table is null
            ? default
            : new SampleEntity()
            {
                Id = table.Id,
                TestProperty = table.TestProperty,
                Active = table.Active,
            };
    }
}