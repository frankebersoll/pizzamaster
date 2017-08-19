using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Core.Caching;
using EventFlow.Logs;

namespace PizzaMaster.Application
{
    public class DictionaryMemoryCache : Cache, IMemoryCache
    {
        private readonly ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        public DictionaryMemoryCache(ILog log) : base(log) { }

        protected override Task<T> GetAsync<T>(CacheKey cacheKey, CancellationToken cancellationToken)
        {
            return this.cache.TryGetValue(cacheKey.Value, out object value)
                       ? Task.FromResult((T) value)
                       : Task.FromResult<T>(null);
        }

        protected override Task SetAsync<T>(
            CacheKey cacheKey,
            DateTimeOffset absoluteExpiration,
            T value,
            CancellationToken cancellationToken)
        {
            this.cache[cacheKey.Value] = value;
            return Task.FromResult(0);
        }

        protected override Task SetAsync<T>(
            CacheKey cacheKey,
            TimeSpan slidingExpiration,
            T value,
            CancellationToken cancellationToken)
        {
            this.cache[cacheKey.Value] = value;
            return Task.FromResult(0);
        }
    }
}