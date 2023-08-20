using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Infra.Data.Context;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys.Base;
using MS.Services.Auth.Infra.IoC.Extensions;

namespace MS.Services.Auth.Infra.IoC;

public class DependencyInjection : BaseDependencyInjection
{
    public override void AddInfraSctructure(IServiceCollection services, IConfiguration configuration)
    {
        AddDbContexts(services, configuration);

        AddRepositorys(services, configuration);
    }

    protected override void AddDbContexts(IServiceCollection services, IConfiguration configuration)
    {
        //É obrigatório definir a versão do My Sql 
        services.AddDbContext<AuthDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
              b => b.MigrationsAssembly(typeof(AuthDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork<AuthDbContext>>();
    }

    protected override void AddMappers(IServiceCollection services, IConfiguration configuration) {}

    protected override void AddRepositorys(IServiceCollection services, IConfiguration configuration) 
    {
        services.AddRepository<User>(); 
    }

    protected override void AddServices(IServiceCollection services, IConfiguration configuration) { }

    protected override void AddValidators(IServiceCollection services, IConfiguration configuration) { }

}
