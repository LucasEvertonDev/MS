
using AutoMapper;
using MS.Libs.Core.Domain.Plugins.IMappers;

namespace ControlServices.Infra.Plugins.AutoMapper;

public class Mapper : IMapperPlugin
{
    private readonly IMapper _mapper;

    public Mapper(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public TDestination Map<TDestination>(object source) where TDestination : class
    {
        return _mapper.Map<TDestination>(source);
    }
}
