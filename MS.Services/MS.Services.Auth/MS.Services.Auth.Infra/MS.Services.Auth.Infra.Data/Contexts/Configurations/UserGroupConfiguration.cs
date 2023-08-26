using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Core.Domain.DbContexts.Enuns;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts.Configurations;

public class UserGroupConfiguration : BaseEntityBasicConfiguration<UserGroup>
{
    public override void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        base.Configure(builder);

        builder.ToTable("UserGroups");

        builder.Property(u => u.Name).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Description).HasMaxLength(50);

        DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<UserGroup> builder)
    {
        builder.HasData(new UserGroup
        {
            Id = new Guid("F97E565D-08AF-4281-BC11-C0206EAE06FA"),
            Name = "Admin",
            Description = "Administrador do sistema",
            Situation = (int)Situation.Active
        },
        new UserGroup
        {
            Id = new Guid("2c2ab8a3-3665-42ef-b4ef-bbec05ac02a5"),
            Name = "Customer",
            Description = "Usuario do sistema",
            Situation = (int)Situation.Active,
        });
    }
}
