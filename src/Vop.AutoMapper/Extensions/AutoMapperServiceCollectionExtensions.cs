using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using Vop.Api;
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
            var mapperAccessor = new MapperAccessor();
            services.AddSingleton<IMapperAccessor>(_ => mapperAccessor);
            services.AddSingleton<MapperAccessor>(_ => mapperAccessor);

            services.Replace(
                 ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider>()
             );

            return services;
        }

        public static IApplicationBuilder UseAutoMapperObjectMapper(this IApplicationBuilder app)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                AssemblyHelper.GetAssemblies().ToList().ForEach(assembly =>
                {
                    AssemblyHelper
                  .GetAllTypes(assembly)
                  .Where(
                      type => type != null &&
                              type.IsClass &&
                              !type.IsAbstract &&
                              !type.IsGenericType &&
                              typeof(Profile).IsAssignableFrom(type)
                  ).ToList().ForEach(type =>
                  {
                      cfg.AddProfile(type);
                  });
                });
            });
            ApiApplication.GetRequiredService<MapperAccessor>().Mapper = mapperConfiguration.CreateMapper();

            return app;
        }
    }
}