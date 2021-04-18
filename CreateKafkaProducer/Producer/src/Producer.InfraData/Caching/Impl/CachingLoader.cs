using Producer.Business.Services;
using Producer.InfraData.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.InfraData.Caching
{
    internal class CachingLoader : ICacheLoader
    {
        private readonly ILookupCacheLoader<ProductTable> _productLoader;
        private readonly ICaching<ProductTable, Guid> _productCaching;

        public CachingLoader(
            ILookupCacheLoader<ProductTable> productLoader,
            ICaching<ProductTable, Guid> productCaching)
        {
            _productLoader = productLoader;
            _productCaching = productCaching;
        }

        public async Task Load()
        {
            var product = await _productLoader.Load();
            _productCaching.Load(product
                .Select(p => (p, p.Id)));
        }
    }
}
