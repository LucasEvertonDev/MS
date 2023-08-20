using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using System.Linq.Expressions;

namespace MS.Libs.Core.Domain.DbContexts.Repositorys;

public interface ISearchRepository<T> where T : IEntity
{
    IQueryable<T> Queryable();

    Task<IEnumerable<T>> ToListAsync();
   
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
}
