namespace MS.Libs.Core.Domain.Plugins.IMappers;

public interface IMapperPlugin
{
    TDestination Map<TDestination>(object source) where TDestination : class;
}
