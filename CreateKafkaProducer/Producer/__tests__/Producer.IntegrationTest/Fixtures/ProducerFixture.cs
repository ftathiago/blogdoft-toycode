using Producer.Business.Entities;
using Producer.Business.Services;
using System.Threading.Tasks;

namespace Producer.IntegrationTest.Fixtures
{
    public class ProducerFixture : IProducer
    {
        public async Task ProduceMessage(SampleEntity entity)
        {
            await Task.Delay(1);
        }
    }
}
