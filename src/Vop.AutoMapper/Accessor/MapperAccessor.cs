using AutoMapper;

namespace Vop.Api.AutoMapper
{
    internal class MapperAccessor : IMapperAccessor
    {
        public IMapper Mapper { get; set; }
    }
}