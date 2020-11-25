using AutoMapper;

namespace Vop.Api.AutoMapper
{
    public interface IMapperAccessor
    {
        IMapper Mapper { get; }
    }
}
