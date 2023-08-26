using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Core.Domain.DbContexts.Enuns;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts.Configurations;

public class MapUserGroupRolesConfiguration : BaseEntityBasicConfiguration<MapUserGroupRoles>
{
    public override void Configure(EntityTypeBuilder<MapUserGroupRoles> builder)
    {
        base.Configure(builder);

        builder.ToTable("MapUserGroupRoles");

        builder.HasOne(m => m.UserGroup)
             .WithMany(UserGroup => UserGroup.MapUserGroupRoles)
             .HasForeignKey(m => m.UserGroupId);

        builder.HasOne(m => m.Role)
            .WithMany(role => role.MapUserGroupRoles)
            .HasForeignKey(m => m.RoleId);

        DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<MapUserGroupRoles> builder)
    {
        builder.HasData(new MapUserGroupRoles
        {
            Id = new Guid("b94afe49-6630-4bf8-a19d-923af259f475"),
            Situation = (int)Situation.Active,
            RoleId = new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"),
            UserGroupId = new Guid("F97E565D-08AF-4281-BC11-C0206EAE06FA"),
        });
    }
}
