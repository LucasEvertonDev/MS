using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Services.Auth.Core.Application.Services.AuthServices;
using MS.Services.Auth.Core.Application.Services.UserServices;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using MS.Services.Auth.Core.Domain.Services.AuthServices;
using MS.Services.Auth.Core.Domain.Services.UserServices;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys;
using MS.Services.Auth.Infra.Data.Contexts;
using MS.Services.Auth.Infra.Plugins.FluentValidation.User;
using MS.Services.Auth.Infra.Plugins.Hasher;
using MS.Services.Auth.Infra.Plugins.TokenJWT;
using Microsoft.EntityFrameworkCore;
using MS.Services.Auth.Infra.Plugins.AutoMapper;
using MS.Services.Auth.Infra.Plugins.AutoMapper.Profiles;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys.Base;

namespace MS.Services.Auth.Test
{

    public class DependencyInjection
    {
        public  void AddInfraSctructure(IServiceCollection services)
        {
            AddDbContexts(services);

            AddRepositorys(services);

            AddMappers(services);

            AddServices(services);

            AddValidators(services);
        }

        protected  void AddDbContexts(IServiceCollection services)
        {
            //É obrigatório definir a versão do My Sql 
            services.AddDbContext<AuthDbContext>(options =>
                  options.UseSqlServer("Server=FIVBR-APP002V\\QA;Database=AuthDb;User Id=user_sign;Password=sign;TrustServerCertificate=True;",
                  b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork, UnitOfWork<AuthDbContext>>();
        }

        protected  void AddMappers(IServiceCollection services)
        {
            services.AddScoped<IMapperPlugin, Mapper>();

            services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper());
        }

        protected  void AddRepositorys(IServiceCollection services)
        {
            services.AddRepository<User>();
            services.AddRepository<Role>();
            services.AddRepository<UserGroup>();
            services.AddRepository<MapUserGroupRoles>();

            services.AddScoped<ISearchMapUserGroupRolesRepository, MapUserGroupRolesRepository>();
        }

        protected  void AddServices(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHash, PasswordHash>();
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<ILoginService, LoginService>();
        }

        protected  void AddValidators(IServiceCollection services)
        {
            services.AddScoped<IValidatorModel<CreateUserModel>, CreateUserValidator>();
        }
    }

    public static class RepositoryExtensions
    {
        public static void AddRepository<TEntity>(this IServiceCollection services) where TEntity : BaseEntityBasic
        {
            services.AddScoped<ICreateRepository<TEntity>, Repository<TEntity>>();
            services.AddScoped<ISearchRepository<TEntity>, Repository<TEntity>>();
            services.AddScoped<IDeleteRepository<TEntity>, Repository<TEntity>>();
            services.AddScoped<IUpdateRepository<TEntity>, Repository<TEntity>>();
        }

        public static void AddRepository<TEntity, TRepository>(this IServiceCollection services) where TEntity : BaseEntityBasic where TRepository : Repository<TEntity>
        {
            services.AddScoped<ICreateRepository<TEntity>, TRepository>();
            services.AddScoped<ISearchRepository<TEntity>, TRepository>();
            services.AddScoped<IDeleteRepository<TEntity>, TRepository>();
            services.AddScoped<IUpdateRepository<TEntity>, TRepository>();
        }
    }
}
