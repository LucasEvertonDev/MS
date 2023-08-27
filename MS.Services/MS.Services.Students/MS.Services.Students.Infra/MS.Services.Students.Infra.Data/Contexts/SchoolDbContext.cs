using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MS.Libs.Infra.Data.Context;
using MS.Services.Students.Core.Domain.DbContexts.Entities;

namespace MS.Services.Students.Infra.Data.Contexts;

public class SchoolDbContext : BaseDbContext<SchoolDbContext>
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options, IHttpContextAccessor httpContext)
       : base(options, httpContext)
    {
        Database.Migrate();
    }

    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(SchoolDbContext).Assembly);
    }
}
