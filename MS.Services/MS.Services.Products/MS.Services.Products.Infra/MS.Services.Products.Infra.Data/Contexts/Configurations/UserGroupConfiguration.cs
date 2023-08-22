using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Products.Core.Domain.DbContexts.Entities;

namespace MS.Services.Products.Infra.Data.Contexts.Configurations;

public class UserGroupConfiguration : BaseEntityBasicConfiguration<UserGroup>
{
    public override void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        base.Configure(builder);

        builder.ToTable("UserGroups");

        builder.Property(u => u.Name).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Description).HasMaxLength(50);
    }
}
