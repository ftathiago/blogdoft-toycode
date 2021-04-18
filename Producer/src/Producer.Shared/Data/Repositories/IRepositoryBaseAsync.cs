using System;
using System.Threading.Tasks;

namespace Producer.Shared.Data.Repositories
{
    public interface IRepositoryBaseAsync<T>
    {
        Task AddAsync(T model);

        Task RemoveAsync(T model);

        Task UpdateAsync(T model);

        Task<T> GetByIdAsync(Guid id);
    }
}
