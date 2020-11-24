using Microsoft.AspNetCore.Builder;
using System;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;
using Vop.Api.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ObjectMappingServiceCollectionExtensions
    {
        public static IServiceCollection AddObjectMapper(this IServiceCollection services)
        {
            services.AddSingleton<IAutoObjectMappingProvider, NotImplementedAutoObjectMappingProvider>();
            services.AddSingleton<IObjectMapper, DefaultObjectMapper>();
            services.AddSingleton(typeof(IObjectMapper<>), typeof(DefaultObjectMapper<>));

            return services;
        }

        public static IApplicationBuilder UseObjectMapper(this IApplicationBuilder app)
        {
            return app;
        }
    }
}