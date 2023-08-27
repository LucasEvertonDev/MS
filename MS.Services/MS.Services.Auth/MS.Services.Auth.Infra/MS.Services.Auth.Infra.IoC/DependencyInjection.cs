using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Serilog;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
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
using MS.Services.Auth.Plugins.AutoMapper.Profiles;
using MS.Services.Auth.Plugins.FluentValidation.User;
using MS.Services.Auth.Plugins.Hasher;
using MS.Services.Auth.Plugins.TokenJWT;
using Serilog;
using Serilog.Core;

namespace MS.Services.Auth.Infra.IoC;

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
        services.AddDbContext<AuthDbContext>(options =>
              options.UseSqlServer(configuration.SqlConnections.DbConnection,
              b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork<AuthDbContext>>();
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
        services.AddRepository<User>();
        services.AddRepository<Role>();
        services.AddRepository<UserGroup>();
        services.AddRepository<MapUserGroupRoles>();
        services.AddRepository<ClientCredentials>();

        services.AddScoped<ISearchMapUserGroupRolesRepository, MapUserGroupRolesRepository>();
    }

    protected override void AddServices(IServiceCollection services, AppSettings configuration) 
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHash, PasswordHash>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        services.AddScoped<ICreateUserService, CreateUserService>();
        services.AddScoped<IUpdateUserService, UpdateUserService>();
        services.AddScoped<IDeleteUserService, DeleteUserService>();
        services.AddScoped<ISearchUserService, SearchUserService>();
        services.AddScoped<IUpdatePasswordService, UpdatePasswordService>();
    }

    protected override void AddValidators(IServiceCollection services, AppSettings configuration)
    {
        services.AddTransient<IValidatorModel<CreateUserModel>, CreateUserValidator>();
        services.AddTransient<IValidatorModel<UpdateUserModel>, UpdateUserValidator>();
        services.AddTransient<IValidatorModel<UpdatePasswordUserModel>, UpdatePasswordUserValidator>();
    }
}
