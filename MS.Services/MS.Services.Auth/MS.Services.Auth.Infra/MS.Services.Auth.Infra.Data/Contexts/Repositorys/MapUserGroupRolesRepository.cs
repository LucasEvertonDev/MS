using Microsoft.EntityFrameworkCore;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Infra.Data.Contexts.Repositorys.Base;

namespace MS.Services.Auth.Infra.Data.Contexts.Repositorys;

public class MapUserGroupRolesRepository : Repository<MapUserGroupRoles>, ISearchMapUserGroupRolesRepository
{
    public MapUserGroupRolesRepository(AuthDbContext applicationDbContext) : base(applicationDbContext)
    {

    }

    public async Task<List<Role>> GetRolesByUserGroup(string userGroupId)
    {
        return await this.Queryable().Include(c => c.Role)
                .Where(p => p.UserGroupId.ToString() == userGroupId)
                .Select(a => a.Role)
                .ToListAsync();
    }
}
