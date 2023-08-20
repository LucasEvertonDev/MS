using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Libs.Infra.Data.Context.Configurations;

public class BaseEntityConfiguration<TEntity> : BaseEntityBasicConfiguration<TEntity> where TEntity : BaseEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(u => u.CreateDate).HasDefaultValueSql("getdate()");
    }
}
