using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.Caching;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DistributedCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedCache(this IServiceCollection services, Action<DistributedCacheOptions> setupAction)
        {
            var option = new DistributedCacheOptions();
            setupAction?.Invoke(option);

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

            services.Configure<DistributedCacheOptions>(cacheOptions =>
            {
                cacheOptions.GlobalCacheEntryOptions.SlidingExpiration = TimeSpan.FromMinutes(20);
            });

            return services;
        }

        public static IApplicationBuilder UseDistributedCache(this IApplicationBuilder app)
        {
            return app;
        }
    }
}