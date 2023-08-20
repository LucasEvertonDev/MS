using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Infra.Data.Context.Repositorys;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Infra.Data.Context;

namespace MS.Services.Auth.Infra.IoC;

public class DependencyInjection : BaseDependencyInjection
{
    public override void AddInfraSctructure(IServiceCollection services, IConfiguration configuration)
    {
        AddDbContexts(services, configuration);
    }

    protected override void AddDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        //É obrigatório definir a versão do My Sql 
        services.AddDbContext<AuthDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
              b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    protected override void AddMappers(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICreateRepository<User>, Repository<User>>();
        services.AddScoped<ILoginRepository, UserRepository>();
        services.AddScoped<ISearchUsersRepository, UserRepository>();


        throw new NotImplementedException();
    }

    protected override void AddRepositorys(IServiceCollection services, IConfiguration configuration)
    {
        
    }

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }

    protected override void AddValidators(IServiceCollection services, IConfiguration configuration)
    {
        throw new NotImplementedException();
    }
}
