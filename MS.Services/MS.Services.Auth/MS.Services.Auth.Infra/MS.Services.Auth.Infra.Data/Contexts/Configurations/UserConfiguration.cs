using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts.Configurations;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(u => u.Username).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Password).HasMaxLength(300).IsRequired();

        builder.Property(u => u.PasswordHash).HasMaxLength(300).IsRequired();

        builder.Property(u => u.Name).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Email).HasMaxLength(50).IsRequired();

        builder.Property(u => u.LastAuthentication);

        builder.HasOne(u => u.UserGroup).WithMany(e => e.Users).HasForeignKey(a => a.UserGroupId).IsRequired();
    }
}

