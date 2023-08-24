using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Infra.Data.Context.Repositorys;

namespace MS.Services.Products.Infra.Data.Contexts.Repositorys.Base;

public class Repository<TEntity> : BaseRepository<ProductsDbContext, TEntity> where TEntity : BaseEntityBasic
{
    public Repository(ProductsDbContext applicationDbContext) : base(applicationDbContext)
    {
    }
}
