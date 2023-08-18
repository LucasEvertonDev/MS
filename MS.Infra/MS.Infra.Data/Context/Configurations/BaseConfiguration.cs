using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MS.Core.Domain.Contexts.Entities.Base;

namespace MS.Infra.Data.Context.Configurations;

public class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase>where TBase : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();
        builder.Property(u => u.CreateDate).HasDefaultValueSql("getdate()");
    }
}

//public class MaintainerConfiguration : BaseEntityTypeConfiguration<Maintainer>
//{
//    public override void Configure(EntityTypeBuilder<Maintainer> entityTypeBuilder)
//    {
//        entityTypeBuilder.Property(b => b.CreatedDateUtc).HasDefaultValueSql("CURRENT_TIMESTAMP");
//        base.Configure(entityTypeBuilder);
//    }
//}
