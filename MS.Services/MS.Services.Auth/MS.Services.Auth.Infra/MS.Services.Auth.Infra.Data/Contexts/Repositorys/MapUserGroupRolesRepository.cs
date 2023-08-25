using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys.Base;

namespace MS.Services.Auth.Infra.Data.Contexts.Repositorys;

public class MapUserGroupRolesRepository : Repository<MapUserGroupRoles>, ISearchMapUserGroupRolesRepository
{
    private readonly IMemoryCache _memoryCache;

    public MapUserGroupRolesRepository(IServiceProvider serviceProvider,
        AuthDbContext applicationDbContext,
        IMemoryCache memoryCache) : base(serviceProvider)
    {
        _memoryCache = memoryCache;
    }

    public async Task<List<Role>> GetRolesByUserGroup(string userGroupId)
    {
        return await this.AsQueriable().Include(c => c.Role)
            .Where(p => p.UserGroupId.ToString() == userGroupId)
            .Select(a => a.Role)
            .ToListAsync();
    }
}
