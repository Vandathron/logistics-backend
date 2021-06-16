using System;
using System.Threading.Tasks;
using logistics_BE.Services.Interface;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace logistics_BE.Services.Implementation
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }


        public async Task CacheResponseAsync(string cacheKey, object response)
        {
            if (response == null) return;

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions());
        }

        public async Task EmptyCache(string cacheKey)
        {
           await _distributedCache.RemoveAsync(cacheKey);
        }

        public async Task<string> GetCacheAsync(string cacheKey)
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);

            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
           
        }
    }
}
