using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MS.Libs.Infra.Data.Context;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using System.Data;

namespace MS.Services.Products.Infra.Data.Contexts;

public class ProductsDbContext : BaseDbContext<ProductsDbContext>
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options, IHttpContextAccessor httpContext)
       : base(options, httpContext)
    {
    }


    public DbSet<Product> Products { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);
    }
}
