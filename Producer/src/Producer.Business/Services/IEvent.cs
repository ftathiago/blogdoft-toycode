using Producer.Business.Entities;
using System.Threading.Tasks;

namespace Producer.Business.Services
{
    public interface IEvent
    {
        Task PublishAsync(SaleEntity entity);
    }
}
