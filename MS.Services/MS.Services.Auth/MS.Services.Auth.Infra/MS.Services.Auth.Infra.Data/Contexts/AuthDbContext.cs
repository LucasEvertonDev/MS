using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MS.Libs.Infra.Data.Context;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts;

public class AuthDbContext : BaseDbContext<AuthDbContext>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options, IHttpContextAccessor httpContext)
       : base(options, httpContext)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<MapUserGroupRoles> MapUserGroupRoles { get; set; }

    public DbSet<UserGroup> UserGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
    }
}
