using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Vop.Api.AutoMapper;
using Vop.Api.FluentException;
using Vop.Api.FluentResult;
using Vop.Api.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapperObjectMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapperAccessor, MapperAccessor>();

            services.Replace(
                 ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider>()
             );

            return services;
        }

        public static IApplicationBuilder UseAutoMapperObjectMapper(this IApplicationBuilder app)
        {
            return app;
        }
    }
}