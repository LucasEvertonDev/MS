﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Libs.Infra.Plugins.AutoMapper;
using MS.Services.Courses.Core.Application.Services.Courseservices;
using MS.Services.Courses.Core.Domain.DbContexts.Entities;
using MS.Services.Courses.Core.Domain.Models.Courses;
using MS.Services.Courses.Core.Domain.Services.Courseservices;
using MS.Services.Courses.Infra.Data.Contexts;
using MS.Services.Courses.Infra.IoC.Extensions;
using MS.Services.Courses.Plugins.AutoMapper.Profiles;

namespace MS.Services.Courses.Infra.IoC;

public class DependencyInjection: BaseDependencyInjection<AppSettings>
{
    public override void AddInfraSctructure(IServiceCollection services, AppSettings configuration)
    {
        AddDbContexts(services, configuration);

        AddRepositorys(services, configuration);

        AddMappers(services, configuration);

        AddServices(services, configuration);

        AddValidators(services, configuration);
    }

    protected override void AddDbContexts(IServiceCollection services, AppSettings configuration)
    {
        //É obrigatório definir a versão do My Sql 
        services.AddDbContext<SchoolDbContext>(options =>
              options.UseSqlServer(configuration.SqlConnections.DbConnection,
              b => b.MigrationsAssembly(typeof(SchoolDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork<SchoolDbContext>>();
    }

    protected override void AddMappers(IServiceCollection services, AppSettings configuration) 
    {
        services.AddScoped<IMapperPlugin, Mapper>();

        services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        }).CreateMapper());
    }

    protected override void AddRepositorys(IServiceCollection services, AppSettings configuration) 
    {
        services.AddRepository<Course>();
    }

    protected override void AddServices(IServiceCollection services, AppSettings configuration)
    { 
        services.AddScoped<ICreateCourseservice, CreateCourseservice>();
        services.AddScoped<IUpdateCourseservice, UpdateCourseservice>();
        services.AddScoped<IDeleteCourseservice, DeleteCourseservice>();
        services.AddScoped<ISearchCourseservice, SearchCourseservice>();
    }

    protected override void AddValidators(IServiceCollection services, AppSettings configuration)
    {
       
    }
}
