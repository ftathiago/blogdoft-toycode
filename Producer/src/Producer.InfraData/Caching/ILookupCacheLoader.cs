using System.Collections.Generic;
using System.Threading.Tasks;

namespace Producer.InfraData.Caching
{
    public interface ILookupCacheLoader<T>
    {
        Task<IEnumerable<T>> Load();
    }
}
