using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Context.Configurations;

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
