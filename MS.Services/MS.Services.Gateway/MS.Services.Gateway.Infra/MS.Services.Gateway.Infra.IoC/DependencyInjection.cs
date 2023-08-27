using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Libs.Infra.Plugins.AutoMapper;
using MS.Services.Gateway.Core.Application.Services.Courseservices;
using MS.Services.Gateway.Core.Domain.DbContexts.Entities;
using MS.Services.Gateway.Core.Domain.Models.Courses;
using MS.Services.Gateway.Core.Domain.Services.Courseservices;
using MS.Services.Gateway.Infra.Data.Contexts;
using MS.Services.Gateway.Plugins.AutoMapper.Profiles;

namespace MS.Services.Gateway.Infra.IoC;

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

    protected override void AddDbContexts(IServiceCollection services, AppSettings configuration){ }

    protected override void AddMappers(IServiceCollection services, AppSettings configuration) 
    {
   
    }

    protected override void AddRepositorys(IServiceCollection services, AppSettings configuration) {}

    protected override void AddServices(IServiceCollection services, AppSettings configuration) { }

    protected override void AddValidators(IServiceCollection services, AppSettings configuration) { }
}
