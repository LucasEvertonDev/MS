using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Students.Core.Domain.DbContexts.Entities;

namespace MS.Services.Students.Infra.Data.Contexts.Configurations;

public class StudentConfiguration : BaseEntitLastUpdateByConfiguration<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        builder.ToTable("Students");

        builder.Property(u => u.Cpf).HasMaxLength(20).IsRequired();

        builder.Property(u => u.Name).HasMaxLength(100).IsRequired();

        builder.Property(u => u.FatherName).HasMaxLength(100).IsRequired();

        builder.Property(u => u.MotherName).HasMaxLength(100).IsRequired();
    }
}

