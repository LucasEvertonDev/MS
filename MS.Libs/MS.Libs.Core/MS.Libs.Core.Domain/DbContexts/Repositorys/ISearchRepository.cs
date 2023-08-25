using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using System.Linq.Expressions;

namespace MS.Libs.Core.Domain.DbContexts.Repositorys;

public interface ISearchRepository<TEntity> where TEntity : IEntity
{
    IQueryable<TEntity> AsQueriable();

    Task<IEnumerable<TEntity>> ToListAsync();
   
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> GetListFromCacheAsync(Expression<Func<TEntity, bool>> predicate);
    Task<Models.Base.PagedResult<TEntity>> ToListAsync(int pageSize, int pageNumber, Expression<Func<TEntity, bool>> predicate);
}
