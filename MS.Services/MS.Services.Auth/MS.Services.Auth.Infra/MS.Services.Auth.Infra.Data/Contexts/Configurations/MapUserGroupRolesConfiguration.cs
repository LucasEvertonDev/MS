﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Context.Configurations;

public class MapUserGroupRolesConfiguration :  BaseEntityBasicConfiguration<MapUserGroupRoles>
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
    }
}