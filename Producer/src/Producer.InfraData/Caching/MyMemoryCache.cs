using Microsoft.Extensions.Caching.Memory;

namespace Producer.InfraData.Caching
{
    public class MyMemoryCache
    {
        public MyMemoryCache()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024,
            });
        }

        public MemoryCache Cache { get; }
    }
}
