using WebApi.Business.Entities;

namespace WebApi.Business.Services
{
    public interface ISampleService
    {
        SampleEntity GetSampleBy(int id);
    }
}