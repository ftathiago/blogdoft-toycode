using Producer.Business.Entities;
using System;
using System.Threading.Tasks;

namespace Producer.Business.Services
{
    public interface ISaleService
    {
        Task<SaleEntity> GetSaleAsync(Guid id);

        Task<SaleEntity> RegisterSale(SaleEntity entity);
    }
}
