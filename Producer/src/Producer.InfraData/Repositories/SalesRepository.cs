using Dapper;
using Producer.Business.Entities;
using Producer.Business.Repositories;
using Producer.InfraData.Exceptions;
using Producer.InfraData.Extensions;
using Producer.InfraData.Models;
using Producer.Shared.Data.Contexts;
using Producer.WarmUp.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.InfraData.Repositories
{
    public class SalesRepository : ISaleRepository, IWarmUpCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public SalesRepository(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public async Task AddAsync(SaleEntity model)
        {
            await _unitOfWork.Connection.ExecuteAsync(
                SampleRepositoryStmt.Insert,
                new
                {
                    model.Id,
                    Document_Id = model.CustomerIdentity,
                    Sale_Number = model.Number,
                    Sale_Date = model.Date,
                },
                _unitOfWork.Transaction);

            foreach (var item in model.Items)
            {
                await _unitOfWork.Connection.ExecuteAsync(
                    SampleRepositoryStmt.InsertItem,
                    new
                    {
                        Id = Guid.NewGuid(),
                        Sale_Id = model.Id,
                        Product_Id = item.Product.Id,
                        item.Value,
                        item.Quantity,
                    },
                    _unitOfWork.Transaction);
            }
        }

        public async Task<SaleEntity> GetByIdAsync(Guid id)
        {
            var sales = await _unitOfWork.Connection
                .QueryAsync<SaleTable, SaleItemSelect, SaleTable>(
                    SampleRepositoryStmt.GetById,
                    (sale, item) =>
                    {
                        sale.Items.Add(SaleItemTable.From(item));
                        return sale;
                    },
                    new { id },
                    _unitOfWork.Transaction,
                    splitOn: "ItemId");
            var sale = sales.FirstOrDefault();
            var entity = sale.AsEntity();
            return entity;
        }

        public async Task RemoveAsync(SaleEntity model)
        {
            var alteredCount = await _unitOfWork.Connection
                .ExecuteAsync(SampleRepositoryStmt.Remove, model.Id);

            if (alteredCount > 1)
            {
                throw new DeleteException("Sample Table", alteredCount, 0);
            }
        }

        public async Task UpdateAsync(SaleEntity model)
        {
            var alteredCount = await _unitOfWork.Connection
                .ExecuteAsync(SampleRepositoryStmt.Update, model);

            if (alteredCount > 1)
            {
                throw new DeleteException("Sample Table", alteredCount, 0);
            }
        }

        async Task IWarmUpCommand.Execute()
        {
            await GetByIdAsync(Guid.Parse("531D7947-84FC-41C6-B8CC-89AF42CE9EC2"));
        }
    }
}
