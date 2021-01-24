using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using Vop.Api;
using Vop.Api.AutoMapper;
using Vop.Api.ObjectMapping;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperAccessor = new MapperAccessor();
            services.AddSingleton<IMapperAccessor>(_ => mapperAccessor);
            services.AddSingleton<MapperAccessor>(_ => mapperAccessor);

            services.Replace(
                 ServiceDescriptor.Transient<IAutoObjectMappingProvider, AutoMapperAutoObjectMappingProvider>()
             );

            return services;
        }

        public static IApplicationBuilder UseAutoMapper(this IApplicationBuilder app)
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