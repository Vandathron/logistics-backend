using System;
using System.Threading.Tasks;

namespace logistics_BE.Services.Interface
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response);

        Task<string> GetCacheAsync(String cacheKey);

        Task EmptyCache(String cacheKey);
    }
}
