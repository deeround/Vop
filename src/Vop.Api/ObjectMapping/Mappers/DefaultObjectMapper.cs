using Microsoft.Extensions.DependencyInjection;
using System;
using Vop.Api.DependencyInjection;

namespace Vop.Api.ObjectMapping
{
    public class DefaultObjectMapper : IObjectMapper
    {
        public IAutoObjectMappingProvider AutoObjectMappingProvider { get; }

        public DefaultObjectMapper(IAutoObjectMappingProvider autoObjectMappingProvider)
        {
            AutoObjectMappingProvider = autoObjectMappingProvider;
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null)
            {
                return default;
            }

            return AutoMap<TSource, TDestination>(source);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null)
            {
                return default;
            }

            return AutoMap(source, destination);
        }

        protected virtual TDestination AutoMap<TSource, TDestination>(object source)
        {
            return AutoObjectMappingProvider.Map<TSource, TDestination>(source);
        }

        protected virtual TDestination AutoMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            return AutoObjectMappingProvider.Map<TSource, TDestination>(source, destination);
        }
    }
}