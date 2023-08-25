﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MS.Libs.Core.Domain.Infra.Attributes;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys.Base;
using System.Reflection;

namespace MS.Services.Auth.Infra.Data.Contexts.Repositorys;

public class MapUserGroupRolesRepository : Repository<MapUserGroupRoles>, ISearchMapUserGroupRolesRepository
{
    private readonly IMemoryCache _memoryCache;

    public MapUserGroupRolesRepository(AuthDbContext applicationDbContext,
        IMemoryCache memoryCache) : base(applicationDbContext)
    {
        _memoryCache = memoryCache;
    }

    public async Task<List<Role>> GetRolesByUserGroup(string userGroupId)
    {
        var (cache, AbsoluteExpirationInMinutes, SlidingExpiration) = GetCustomAttributes();

        var role = await this.AsQueriable().Include(c => c.Role)
                       .Where(p => p.UserGroupId.ToString() == userGroupId)
                       .Select(a => a.Role)
                       .ToListAsync();

        role.AddRange(role);

        role.AddRange(role);
        role.AddRange(role);



        try
        {
            return await _memoryCache.GetOrCreateAsync(
                cache,
                async cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration);

                    cacheEntry.AbsoluteExpiration = DateTime.Now.AddMinutes(AbsoluteExpirationInMinutes);

                    var options = new MemoryCacheOptions();

                    return role;
                });
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public (string, long, long) GetCustomAttributes()
    {
        var cache = typeof(MapUserGroupRoles).GetCustomAttributes<CacheAttribute>().FirstOrDefault();

        return (cache.Key, cache.AbsoluteExpirationInMinutes ?? 1, cache.SlidingExpirationInMinutes ?? 1);
    }
}
