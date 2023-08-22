using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.Plugins.AutoMapper;
using MS.Services.Auth.Core.Application.Services.AuthServices;
using MS.Services.Auth.Core.Application.Services.UserServices;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using MS.Services.Auth.Core.Domain.Services.AuthServices;
using MS.Services.Auth.Core.Domain.Services.UserServices;
using MS.Services.Auth.Infra.Data.Contexts;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys;
using MS.Services.Auth.Infra.IoC.Extensions;
using MS.Services.Auth.Infra.Plugins.AutoMapper.Profiles;
using MS.Services.Auth.Infra.Plugins.FluentValidation.User;
using MS.Services.Auth.Infra.Plugins.Hasher;
using MS.Services.Auth.Infra.Plugins.TokenJWT;

namespace MS.Services.Auth.Infra.IoC;

public class DependencyInjection
{
    public void AddInfraSctructure(IServiceCollection services, AppSettings configuration)
    {
        AddDbContexts(services, configuration);

        AddRepositorys(services, configuration);

        AddMappers(services, configuration);

        AddServices(services, configuration);

        AddValidators(services, configuration); 
    }

    protected void AddDbContexts(IServiceCollection services, AppSettings configuration)
    {
        //É obrigatório definir a versão do My Sql 
        services.AddDbContext<AuthDbContext>(options =>
              options.UseSqlServer(configuration.DbConnection,
              b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork<AuthDbContext>>();
    }

    protected void AddMappers(IServiceCollection services, AppSettings configuration) 
    {
        services.AddScoped<IMapperPlugin, Mapper>();

        services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        }).CreateMapper());
    }

    protected void AddRepositorys(IServiceCollection services, AppSettings configuration) 
    {
        services.AddRepository<User>();
        services.AddRepository<Role>();
        services.AddRepository<UserGroup>();
        services.AddRepository<MapUserGroupRoles>();

        services.AddScoped<ISearchMapUserGroupRolesRepository, MapUserGroupRolesRepository>();
    }

    protected void AddServices(IServiceCollection services, AppSettings configuration) 
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHash, PasswordHash>();
        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<ILoginService, LoginService>();
    }

    protected void AddValidators(IServiceCollection services, AppSettings configuration)
    {
        services.AddScoped<IValidatorModel<CreateUserModel>, CreateUserValidator>();
    }
}
