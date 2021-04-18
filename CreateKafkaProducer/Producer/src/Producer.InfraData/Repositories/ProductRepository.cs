using Dapper;
using Producer.Business.Entities;
using Producer.Business.Repositories;
using Producer.InfraData.Caching;
using Producer.InfraData.Models;
using Producer.Shared.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Producer.InfraData.Repositories
{
    internal class ProductRepository : IProductRepository, ILookupCacheLoader<ProductTable>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICaching<ProductTable, Guid> _cache;

        public ProductRepository(
            IUnitOfWork unitOfWork,
            ICaching<ProductTable, Guid> cache) =>
            (_unitOfWork, _cache) = (unitOfWork, cache);

        public async Task<IEnumerable<ProductTable>> Load() =>
            await _unitOfWork.Connection.QueryAsync<ProductTable>(
                "select * from Products",
                transaction: _unitOfWork.Transaction);

        public async Task<Product> GetById(Guid id)
        {
            var productTable = await _cache.GetById(
                id,
                () => _unitOfWork.Connection.QueryFirstOrDefaultAsync<ProductTable>(
                "Select * from Products where id = @id",
                new { Id = id },
                _unitOfWork.Transaction));

            return From(productTable);
        }

        private static Product From(ProductTable table) => new(
            id: table.Id,
            description: table.Description,
            value: table.Value);
    }
}
