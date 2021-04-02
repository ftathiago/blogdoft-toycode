using Dapper;
using WebApi.Business.Entities;
using WebApi.Business.Repositories;
using WebApi.InfraData.Exceptions;
using WebApi.InfraData.Extensions;
using WebApi.InfraData.Models;
using WebApi.Shared.Data.Contexts;

namespace WebApi.InfraData.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public SampleRepository(IUnitOfWork unitOfWork) =>
            _unitOfWork = unitOfWork;

        public void Add(SampleEntity model) => _unitOfWork.Connection
            .Execute(SampleRepositoryStmt.Insert, model);

        public SampleEntity GetById(int id) =>
            _unitOfWork.Connection
                .QueryFirstOrDefault<SampleTable>(
                    SampleRepositoryStmt.GetById,
                    new { id, },
                    _unitOfWork.Transaction)?
                .AsEntity();

        public void Remove(SampleEntity model)
        {
            var alteredCount = _unitOfWork.Connection
                .Execute(SampleRepositoryStmt.Remove, model.Id);

            if (alteredCount > 1)
            {
                throw new DeleteException("Sample Table", alteredCount, 0);
            }
        }

        public void Update(SampleEntity model)
        {
            var alteredCount = _unitOfWork.Connection
                .Execute(SampleRepositoryStmt.Update, model);

            if (alteredCount > 1)
            {
                throw new DeleteException("Sample Table", alteredCount, 0);
            }
        }
    }
}