using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts.Configurations;

public class AnimalConfiguration : BaseEntityBasicConfiguration<Animal>
{
    public override void Configure(EntityTypeBuilder<Animal> builder)
    {
        base.Configure(builder);

        builder.UseTptMappingStrategy();

        builder.ToTable("Animal");

        builder.Property(u => u.Name).HasMaxLength(20).IsRequired();
    }
}
