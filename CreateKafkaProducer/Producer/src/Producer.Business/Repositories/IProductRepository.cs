using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Producer.Business.Entities;

namespace Producer.Business.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(Guid id);
    }
}
