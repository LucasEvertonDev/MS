using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.Enuns;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Infra.AppSettings;
using MS.Libs.Core.Domain.Infra.Attributes;
using MS.Libs.Core.Domain.Models.Base;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace MS.Libs.Infra.Data.Context.Repositorys;

public class BaseRepository<TContext, TEntity> : ICreateRepository<TEntity>, IDeleteRepository<TEntity>,
    IUpdateRepository<TEntity>, ISearchRepository<TEntity> where TEntity : BaseEntityBasic where TContext : DbContext
{
    protected TContext _applicationDbContext;
    private readonly IMemoryCache _memoryCache;
    private readonly AppSettings _appSettings;

    public BaseRepository(IServiceProvider serviceProvider)
    {
        _applicationDbContext = serviceProvider.GetService<TContext>();
        _memoryCache = serviceProvider.GetService<IMemoryCache>();
        _appSettings = serviceProvider.GetService<AppSettings>();
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

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var remove = await this.AsQueriable().Where(predicate).ToListAsync();

        _applicationDbContext.Remove(remove);
    }

    public virtual async Task DeleteLogicAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var remove = await this.AsQueriable().Where(predicate).ToListAsync();

        if (remove != null)
        {
            for (var index = 0; index < remove.Count; index++)
            {
                remove[index].Situation = (int)Situation.Deleted;

                await this.UpdateAsync(remove[index]);
            }
        }
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
    public virtual IQueryable<TEntity> AsQueriable()
    {
        return _applicationDbContext.Set<TEntity>().AsNoTracking();
    }

    public virtual async Task<List<TEntity>> GetListFromCacheAsync(Func<TEntity, bool> predicate)
    {
        try
        {
            var (cache, AbsoluteExpirationInMinutes, SlidingExpirationInMinutes) = GetCustomAttributes();

            var itens =  await _memoryCache.GetOrCreateAsync(cache,
                async (cacheEntry) =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(SlidingExpirationInMinutes);
                    cacheEntry.AbsoluteExpiration = DateTime.Now.AddMinutes(AbsoluteExpirationInMinutes);

                    return await _applicationDbContext.Set<TEntity>().AsNoTracking().ToListAsync();
                });

            return itens.Where(predicate).ToList();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<PagedResult<TEntity>> ToListAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate)
    {
        var count = await _applicationDbContext.Set<TEntity>().AsNoTracking().Where(predicate).CountAsync();

        var itens =  await _applicationDbContext.Set<TEntity>().AsNoTracking().Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>(itens, pageNumber, pageSize, count);
    }

    private (string, long, long) GetCustomAttributes()
    {
        var cache = typeof(TEntity).GetCustomAttributes<CacheAttribute>().FirstOrDefault();

        return (cache.Key, cache.AbsoluteExpirationInMinutes ?? _appSettings.MemoryCache.AbsoluteExpirationInMinutes,
            cache.SlidingExpirationInMinutes ?? _appSettings.MemoryCache.SlidingExpirationInMinutes);
    }
}
