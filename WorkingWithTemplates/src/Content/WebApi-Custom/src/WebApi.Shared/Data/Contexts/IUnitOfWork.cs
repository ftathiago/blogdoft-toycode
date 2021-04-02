using System.Data;

namespace WebApi.Shared.Data.Contexts
{
    public interface IUnitOfWork
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}