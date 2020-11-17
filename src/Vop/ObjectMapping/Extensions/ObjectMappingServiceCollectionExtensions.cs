using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ObjectMappingServiceCollectionExtensions
    {
        public static IServiceCollection AddObjectMapping(this IServiceCollection services)
        {
            return services;
        }

        public static IApplicationBuilder UseObjectMapping(this IApplicationBuilder app)
        {
            return app;
        }
    }
}