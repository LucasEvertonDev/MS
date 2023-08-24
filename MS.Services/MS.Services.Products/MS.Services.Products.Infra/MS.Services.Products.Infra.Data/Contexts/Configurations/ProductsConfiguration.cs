using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Products.Core.Domain.DbContexts.Entities;

namespace MS.Services.Products.Infra.Data.Contexts.Configurations;

public class ProductsConfiguration : BaseEntitLastUpdateByConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.ToTable("Products");

        builder.Property(u => u.Name).HasMaxLength(50).IsRequired();

        builder.Property(u => u.Description).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Price).IsRequired();
    }
}
