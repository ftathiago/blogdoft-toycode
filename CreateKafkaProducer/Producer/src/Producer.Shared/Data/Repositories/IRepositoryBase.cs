using System;

namespace Producer.Shared.Data.Repositories
{
    public interface IRepositoryBase<T>
    {
        void Add(T model);

        void Remove(T model);

        void Update(T model);

        T GetById(Guid id);
    }
}
