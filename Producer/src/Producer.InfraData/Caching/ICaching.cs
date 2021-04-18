using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Producer.InfraData.Caching
{
    public interface ICaching<TTable, TKey>
    {
        void Set(TTable lookupTable, TKey id);

        Task<TTable> GetById(TKey id, Func<Task<TTable>> searchRepository);

        void Load(IEnumerable<(TTable Table, TKey Key)> lookupTable);
    }
}
