using System;
using System.Net;
using WebApi.Business.Entities;
using WebApi.Business.Repositories;
using WebApi.Shared.Data.Contexts;
using WebApi.Shared.Holders;

namespace WebApi.Business.Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageHolder _messageHolder;

        public SampleService(
            ISampleRepository repository,
            IUnitOfWork unitOfWork,
            IMessageHolder messageHolder)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _messageHolder = messageHolder;
        }

        public SampleEntity GetSampleBy(int id)
        {
            // Open a transaction is not really necessary. It's just an example of how to use UoW.
            try
            {
                _unitOfWork.BeginTransaction();

                var sample = _repository.GetById(id);

                _unitOfWork.Commit();

                return sample;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _messageHolder.AddMessage(HttpStatusCode.InternalServerError, ex.Message);
                return null;
            }
        }
    }
}