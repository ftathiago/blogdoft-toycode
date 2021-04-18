using Consumer.Business.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consumer.Business.Services
{
    public class SaleEventHandler : ISaleEventHandler
    {
        public Task Handle(IDictionary<string, string> headers, SaleEvent message)
        {
            Console.WriteLine($"Sale {message.Id} processed");
            return Task.CompletedTask;
        }
    }
}
