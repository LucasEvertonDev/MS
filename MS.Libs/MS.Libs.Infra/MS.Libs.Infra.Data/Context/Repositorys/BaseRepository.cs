using Microsoft.EntityFrameworkCore;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Enuns;
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

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var remove = await this.AsQueriable().Where(predicate).ToListAsync();

        _applicationDbContext.Remove(remove);
    }

    public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _applicationDbContext.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public virtual async Task<IEnumerable<TEntity>> ToListAsync()
    {
        return await _applicationDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _applicationDbContext.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual Task<TEntity> CreateAsync(TEntity domain)
    {
        _applicationDbContext.Entry(domain).State = EntityState.Added;

        _applicationDbContext.AddAsync(domain);

        return Task.FromResult(domain);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity domain)
    {
        _applicationDbContext.Entry(domain).State = EntityState.Modified;

        _applicationDbContext.Update(domain);

        return Task.FromResult(domain);
    }

    public virtual IQueryable<TEntity> AsQueriable()
    {
        return _applicationDbContext.Set<TEntity>().AsNoTracking();
    }
}
