using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Data.Context.UnitOfWork;
using MS.Libs.Infra.IoC;
using MS.Libs.Infra.Plugins.AutoMapper;
using MS.Services.Products.Core.Application.Services.Products;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using MS.Services.Products.Core.Domain.Models.Auth;
using MS.Services.Products.Core.Domain.Services.ProductsService;
using MS.Services.Products.Infra.Data.Contexts;
using MS.Services.Products.Infra.IoC.Extensions;
using MS.Services.Products.Infra.Plugins.AutoMapper.Profiles;
using MS.Services.Products.Infra.Plugins.FluentValidation.User;

namespace MS.Services.Products.Infra.IoC;

public class DependencyInjection : BaseDependencyInjection<AppSettings>
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
        services.AddDbContext<ProductsDbContext>(options =>
              options.UseSqlServer(configuration.DbConnection,
              b => b.MigrationsAssembly(typeof(ProductsDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork<ProductsDbContext>>();
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
        services.AddRepository<Product>();
    }

    protected override void AddServices(IServiceCollection services, AppSettings configuration) 
    {
        services.AddScoped<ICreateProductSetvice, CreateProductsService>();
        services.AddScoped<IUpdateProductService, UpdateProductService>();
    }

    protected override void AddValidators(IServiceCollection services, AppSettings configuration)
    {
        services.AddScoped<IValidatorModel<CreateProductModel>, CreateProductsValidator>();
    }
}
