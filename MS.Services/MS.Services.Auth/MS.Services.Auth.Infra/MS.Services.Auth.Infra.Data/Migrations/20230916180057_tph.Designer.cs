﻿// <auto-generated />
using System;
using MS.Services.Auth.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MS.Services.Auth.Infra.Data.Migrations
{
    [DbContext(typeof(AuthDbContext))]
    [Migration("20230916180057_tph")]
    partial class tph
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Animal", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.ClientCredentials", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ClientDescription")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ClientSecret")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ClientsCredentials", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                            ClientDescription = "Cliente padrão da aplicação",
                            ClientId = "7064bbbf-5d11-4782-9009-95e5a6fd6822",
                            ClientSecret = "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910",
                            CreateDate = new DateTime(2023, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Situation = 1
                        });
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Charge")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<int>("Months")
                        .HasColumnType("int");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Contracts", (string)null);

                    b.HasDiscriminator<string>("ClassName").HasValue("Contract");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.MapUserGroupRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<Guid>("UserGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserGroupId");

                    b.ToTable("MapUserGroupRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b94afe49-6630-4bf8-a19d-923af259f475"),
                            RoleId = new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"),
                            Situation = 1,
                            UserGroupId = new Guid("f97e565d-08af-4281-bc11-c0206eae06fa")
                        });
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"),
                            Name = "CHANGE_STUDENTS",
                            Situation = 1
                        });
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("LastAuthentication")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Situation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.ToTable("UserGroups", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f97e565d-08af-4281-bc11-c0206eae06fa"),
                            Description = "Administrador do sistema",
                            Name = "Admin",
                            Situation = 1
                        },
                        new
                        {
                            Id = new Guid("2c2ab8a3-3665-42ef-b4ef-bbec05ac02a5"),
                            Description = "Usuario do sistema",
                            Name = "Customer",
                            Situation = 1
                        });
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Bird", b =>
                {
                    b.HasBaseType("MS.Services.Auth.Core.Domain.DbContexts.Entities.Animal");

                    b.Property<int>("Wingspan")
                        .HasColumnType("int");

                    b.ToTable("Birds");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Mammal", b =>
                {
                    b.HasBaseType("MS.Services.Auth.Core.Domain.DbContexts.Entities.Animal");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Mammals");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.BroadBandContract", b =>
                {
                    b.HasBaseType("MS.Services.Auth.Core.Domain.DbContexts.Entities.Contract");

                    b.Property<int>("DownloadSpeed")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("MS.Services.Auth.Core.Domain.DbContexts.Entities.BroadBandContract");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.MobileContract", b =>
                {
                    b.HasBaseType("MS.Services.Auth.Core.Domain.DbContexts.Entities.Contract");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("MS.Services.Auth.Core.Domain.DbContexts.Entities.MobileContract");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.TvContract", b =>
                {
                    b.HasBaseType("MS.Services.Auth.Core.Domain.DbContexts.Entities.Contract");

                    b.Property<int>("PackageType")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("MS.Services.Auth.Core.Domain.DbContexts.Entities.TvContract");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.MapUserGroupRoles", b =>
                {
                    b.HasOne("MS.Services.Auth.Core.Domain.DbContexts.Entities.Role", "Role")
                        .WithMany("MapUserGroupRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MS.Services.Auth.Core.Domain.DbContexts.Entities.UserGroup", "UserGroup")
                        .WithMany("MapUserGroupRoles")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.User", b =>
                {
                    b.HasOne("MS.Services.Auth.Core.Domain.DbContexts.Entities.UserGroup", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserGroup");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Bird", b =>
                {
                    b.HasOne("MS.Services.Auth.Core.Domain.DbContexts.Entities.Animal", null)
                        .WithOne()
                        .HasForeignKey("MS.Services.Auth.Core.Domain.DbContexts.Entities.Bird", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Mammal", b =>
                {
                    b.HasOne("MS.Services.Auth.Core.Domain.DbContexts.Entities.Animal", null)
                        .WithOne()
                        .HasForeignKey("MS.Services.Auth.Core.Domain.DbContexts.Entities.Mammal", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.Role", b =>
                {
                    b.Navigation("MapUserGroupRoles");
                });

            modelBuilder.Entity("MS.Services.Auth.Core.Domain.DbContexts.Entities.UserGroup", b =>
                {
                    b.Navigation("MapUserGroupRoles");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
