using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MS.Libs.Infra.Data.Context;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using System.Data;

namespace MS.Services.Auth.Infra.Data.Context;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
       : base(options)
    { }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<MapUserGroupRoles> MapUserGroupRoles { get; set; }

    public DbSet<UserGroup> UserGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (EntityEntry item in from entry in ChangeTracker.Entries()
                                     where entry.Entity.GetType().GetProperty("UpdateDate") != null
                                     select entry)
        {
            if (item.State == EntityState.Modified)
            {
                if (item.Property("UpdateDate").CurrentValue == null)
                {
                    item.Property("UpdateDate").CurrentValue = DateTime.Now;
                }
            }
            else if (item.State == EntityState.Added && item.Property("CreateDate").CurrentValue == null)
            {
                item.Property("CreateDate").CurrentValue = DateTime.Now;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
