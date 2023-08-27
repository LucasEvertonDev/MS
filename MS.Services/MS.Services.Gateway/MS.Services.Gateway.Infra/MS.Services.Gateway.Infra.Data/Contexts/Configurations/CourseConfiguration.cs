using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Gateway.Core.Domain.DbContexts.Entities;

namespace MS.Services.Gateway.Infra.Data.Contexts.Configurations;

public class CourseConfiguration : BaseEntitLastUpdateByConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        base.Configure(builder);

        builder.ToTable("Courses");

        builder.Property(u => u.Name).HasMaxLength(100).IsRequired();

        builder.Property(u => u.StartDate).IsRequired();

        builder.Property(u => u.EndDate).IsRequired();
    }
}

