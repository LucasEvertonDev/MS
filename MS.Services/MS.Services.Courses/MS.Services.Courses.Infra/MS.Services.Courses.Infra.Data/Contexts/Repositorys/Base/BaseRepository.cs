using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Infra.Data.Context.Repositorys;

namespace MS.Services.Courses.Infra.Data.Contexts.Repositorys.Base;

public class Repository<TEntity> : BaseRepository<SchoolDbContext, TEntity> where TEntity : BaseEntityBasic
{
    public Repository(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}
