using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Core.Domain.Services;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Libs.Infra.Plugins.AutoMapper;
using MS.Services.Auth.Core.Application.Services.AuthServices;
using MS.Services.Auth.Core.Application.Services.UserServices;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using MS.Services.Auth.Infra.Data.Contexts;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys.Base;
using MS.Services.Auth.Infra.IoC.Extensions;
using MS.Services.Auth.Infra.Plugins.AutoMapper.Profiles;
using MS.Services.Auth.Infra.Plugins.FluentValidation.User;
using MS.Services.Auth.Infra.Plugins.Hasher;
using MS.Services.Auth.Infra.Plugins.TokenJWT;

namespace MS.Services.Auth.Infra.IoC;

public class DependencyInjection : BaseDependencyInjection
{
    public override void AddInfraSctructure(IServiceCollection services, IConfiguration configuration)
    {
        AddDbContexts(services, configuration);

        AddRepositorys(services, configuration);

        AddMappers(services, configuration);

        AddServices(services, configuration);

        AddValidators(services, configuration); 
    }

    protected override void AddDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        //É obrigatório definir a versão do My Sql 
        services.AddDbContext<AuthDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
              b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork<AuthDbContext>>();
    }

    protected override void AddMappers(IServiceCollection services, IConfiguration configuration) 
    {
        services.AddScoped<IMapperPlugin, Mapper>();

        services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        }).CreateMapper());
    }

    protected override void AddRepositorys(IServiceCollection services, IConfiguration configuration) 
    {
        services.AddRepository<User>();
        services.AddRepository<Role>();
        services.AddRepository<UserGroup>();
        services.AddRepository<MapUserGroupRoles>();

        services.AddScoped<ISearchMapUserGroupRolesRepository, MapUserGroupRolesRepository>();
        services.AddScoped<ICreateService<UserModel>, CreateUserService>();
        services.AddScoped<ICreateService<UserModel>, CreateUserService>();
    }

    protected override void AddServices(IServiceCollection services, IConfiguration configuration) 
    {
        services.AddScoped<ICreateService<UserModel>, CreateUserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHash, PasswordHash>();


        services.AddScoped<IActionService<AuthModel, TokenModel>, LoginService>();
    }

    protected override void AddValidators(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidatorModel<UserModel>, CreateUserValidator>();
    }
}
