﻿using Vop.Api.ObjectMapping;

namespace Vop.Api.AutoMapper
{
    public class AutoMapperAutoObjectMappingProvider : IAutoObjectMappingProvider
    {
        public IMapperAccessor MapperAccessor { get; }

        public AutoMapperAutoObjectMappingProvider(IMapperAccessor mapperAccessor)
        {
            MapperAccessor = mapperAccessor;
        }

        public virtual TDestination Map<TSource, TDestination>(object source)
        {
            return MapperAccessor.Mapper.Map<TDestination>(source);
        }

        public virtual TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return MapperAccessor.Mapper.Map(source, destination);
        }
    }
}
