using Producer.Business.Entities;
using Producer.Business.Exceptions;
using Producer.Business.Repositories;
using Producer.Shared.Data.Contexts;
using Producer.Shared.Holders;
using Producer.WarmUp.Abstractions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Producer.Business.Services
{
    public class SalesService : ISaleService, IWarmUpCommand
    {
        private readonly ISaleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageHolder _messageHolder;
        private readonly IEvent _producer;

        public SalesService(
            ISaleRepository repository,
            IUnitOfWork unitOfWork,
            IMessageHolder messageHolder,
            IEvent producer)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _messageHolder = messageHolder;
            _producer = producer;
        }

        public async Task<SaleEntity> GetSaleAsync(Guid id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _messageHolder.AddMessage(HttpStatusCode.InternalServerError, ex.Message);
                return default;
            }
        }

        public async Task<SaleEntity> RegisterSale(SaleEntity entity)
        {
            if (!entity.IsValid())
            {
                throw new InvalidEntityException(
                    nameof(SaleEntity),
                    entity.GetValidations());
            }

            _unitOfWork.BeginTransaction();
            try
            {
                await _repository.AddAsync(entity);
                _unitOfWork.Commit();
                await _producer.PublishAsync(entity);
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }

            return entity;
        }

        async Task IWarmUpCommand.Execute()
        {
            await GetSaleAsync(Guid.Empty);
        }
    }
}
