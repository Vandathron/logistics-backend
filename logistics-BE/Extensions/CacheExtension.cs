using System;
using logistics_BE.Cache;
using logistics_BE.Services.Implementation;
using logistics_BE.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace logistics_BE.Extensions
{
    public static class CacheExtension
    {
        public static void AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            var redisCacheSettings = new RedisCacheSettings();

            configuration.GetSection(key: nameof(RedisCacheSettings)).Bind(redisCacheSettings);
            services.AddSingleton(redisCacheSettings);

            if (!redisCacheSettings.Enabled) return;

            services.AddStackExchangeRedisCache(option => option.Configuration = redisCacheSettings.ConnectionString);

            services.AddSingleton<IResponseCacheService, ResponseCacheService>();
        }
    }
}
