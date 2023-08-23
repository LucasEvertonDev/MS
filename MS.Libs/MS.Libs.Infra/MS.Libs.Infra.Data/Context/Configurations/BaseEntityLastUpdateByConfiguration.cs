using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Libs.Infra.Data.Context.Configurations;

public class BaseEntitLastUpdateByConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : BaseEntityLastUpdateBy
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(u => u.LastUpdateBy).HasMaxLength(50);
    }
}
