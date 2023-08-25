using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MS.Libs.Core.Domain.Infra.Claims;
using MS.Libs.Infra.Data.Context;
using MS.Libs.Infra.Utils.Extensions;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using System.Data;
using System.Net.Http;

namespace MS.Services.Products.Infra.Data.Contexts;

public class ProductsDbContext : BaseDbContext<ProductsDbContext>
{
    private readonly IHttpContextAccessor _httpContext;

    public ProductsDbContext(DbContextOptions<ProductsDbContext> options, IHttpContextAccessor httpContext)
       : base(options, httpContext)
    {
        _httpContext = httpContext;
    }


    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("UpdateDate") != null))
        {
            if (entry.State == EntityState.Modified)
            {
                if (entry.Property("UpdateDate").CurrentValue == null)
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
            }
            else if (entry.State == EntityState.Added)
            {
                if (entry.Property("CreateDate").CurrentValue == null)
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
            }
        }

        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("LastUpdateBy") != null))
        {
            if (entry.State == EntityState.Modified)
            {
                var userid = _httpContext.HttpContext?.User?.Identity?.GetUserClaim(JWTUserClaims.UserId);

                if (entry.Property("LastUpdateBy").CurrentValue == null && !string.IsNullOrEmpty(userid))
                    entry.Property("LastUpdateBy").CurrentValue = userid;
            }

        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
