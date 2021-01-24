using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.Caching;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DistributedCacheServiceCollectionExtensions
    {
        public static IServiceCollection AddDistributedCache(this IServiceCollection services, DistributedCacheOptions option)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            services.AddSingleton<IDistributedCacheSerializer, Utf8JsonDistributedCacheSerializer>();
            services.AddSingleton<IDistributedCacheKeyNormalizer, DistributedCacheKeyNormalizer>();
            services.AddSingleton(typeof(IDistributedCache<>), typeof(DistributedCache<>));
            services.AddSingleton(typeof(IDistributedCache<,>), typeof(DistributedCache<,>));

            return services;
        }

        public static IApplicationBuilder UseDistributedCache(this IApplicationBuilder app)
        {
            return app;
        }
    }
}