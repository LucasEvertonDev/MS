using Microsoft.EntityFrameworkCore;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using System.Linq.Expressions;

namespace MS.Libs.Infra.Data.Context.Repositorys;

public class BaseRepository<TContext, TEntity> : ICreateRepository<TEntity>, IDeleteRepository<TEntity>, IUpdateRepository<TEntity>, ISearchRepository<TEntity> where TEntity : BaseEntityBasic where TContext : DbContext
{
    protected TContext _applicationDbContext;

    public BaseRepository(TContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    public virtual Task<TEntity> Delete(TEntity domain)
    {
        _applicationDbContext.Remove(domain);
        return Task.FromResult(domain);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> FindById(Expression<Func<TEntity, bool>> predicate)
    {
        return await _applicationDbContext.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate); ;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TEntity>> FindAll()
    {
        return await _applicationDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    public virtual Task<TEntity> CreateAsync(TEntity domain)
    {
        _applicationDbContext.Entry(domain).State = EntityState.Added;

        _applicationDbContext.AddAsync(domain);
        return Task.FromResult(domain);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    public virtual Task<TEntity> Update(TEntity domain)
    {
        _applicationDbContext.Entry(domain).State = EntityState.Modified;

        _applicationDbContext.Update(domain);

        return Task.FromResult(domain);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public virtual IQueryable<TEntity> Get()
    {
        return _applicationDbContext.Set<TEntity>().AsNoTracking();
    }
}
