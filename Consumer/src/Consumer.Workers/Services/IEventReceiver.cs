using Consumer.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.Business.Services
{
    public interface IEventReceiver
    {
        Task Execute(
            Func<IDictionary<string, string>, SaleEvent, Task> messageHandler,
            CancellationToken token = default);
    }
}
