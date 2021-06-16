using System;
using System.Threading.Tasks;
using logistics_BE.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace logistics_BE.Cache
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {

        public CachedAttribute()
        {
        }

        public async  Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {

                var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<RedisCacheSettings>();
                // don't continue if caching is disabled
                if (!cacheSettings.Enabled)
                {
                    await next();
                    return;
                }

                var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
                var cacheKey = "LATEST-RESULT";

                var cachedResponse = await cacheService.GetCacheAsync(cacheKey);

                if (!string.IsNullOrEmpty(cachedResponse))
                {
                    var contentResult = new ContentResult
                    {
                        StatusCode = 200,
                        ContentType = "application/json",
                        Content = cachedResponse
                    };

                    context.Result = contentResult;
                    return;
                }

                var executedContext = await next();

                if (executedContext.Result is OkObjectResult objectResult)
                {
                    await cacheService.CacheResponseAsync(cacheKey, objectResult.Value);
                }


                await next();
            }
            catch (Exception ex)
            {

            } 

            // after
        }
    }
}
