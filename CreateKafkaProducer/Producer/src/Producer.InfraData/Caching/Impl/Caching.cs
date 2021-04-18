using Microsoft.Extensions.Caching.Memory;
using Producer.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Producer.InfraData.Caching
{
    public class Caching<TTable, TKey> : ICaching<TTable, TKey>
    {
        private const string CacheKey = "{0}:{1}";
        private readonly IMemoryCache _cache;

        public Caching(IMemoryCache cache) =>
            _cache = cache;

        public async Task<TTable> GetById(TKey id, Func<Task<TTable>> searchRepository)
        {
            var cached = _cache.TryGetValue<TTable>(
                CacheKey.Format(nameof(TTable), id),
                out var lookupTable);
            if (!cached)
            {
                lookupTable = await searchRepository();
                Set(lookupTable, id);
            }

            return lookupTable;
        }

        public void Set(TTable lookupTable, TKey id)
        {
            if (lookupTable is null)
            {
                return;
            }

            var tomorrowMidNight = DateTime.Today.AddDays(1);
            _cache.Set(CacheKey.Format(nameof(TTable), id), lookupTable, tomorrowMidNight);
        }

        public void Load(IEnumerable<(TTable Table, TKey Key)> lookupTable)
        {
            foreach (var (table, key) in lookupTable)
            {
                Set(table, key);
            }
        }
    }
}
