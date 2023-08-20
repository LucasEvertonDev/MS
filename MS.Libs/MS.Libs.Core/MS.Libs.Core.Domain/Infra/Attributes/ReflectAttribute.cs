using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Libs.Core.Domain.Infra.Attributes;

public class ReflectAttribute<TEntity> : Attribute where TEntity : IEntity
{
    public Type EntityType => typeof(TEntity);
}
