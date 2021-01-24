using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using Vop.Api;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;
using Vop.Api.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, RedisCacheOptions option)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options = option;
            });

            return services;
        }

        public static IApplicationBuilder UseRedisCache(this IApplicationBuilder app)
        {
            return app;
        }
    }
}