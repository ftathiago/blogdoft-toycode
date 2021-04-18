using Consumer.Business.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consumer.Business.Services
{
    public interface ISaleEventHandler
    {
        Task Handle(IDictionary<string, string> headers, SaleEvent message);
    }
}
