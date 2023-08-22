using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Products.Core.Domain.DbContexts.Entities;

namespace MS.Services.Products.Infra.Data.Contexts.Configurations;

public class RoleConfiguration : BaseEntityBasicConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder)
    {
        base.Configure(builder);

        builder.ToTable("Roles");

        builder.Property(u => u.Name).HasMaxLength(30).IsRequired();
    }
}
