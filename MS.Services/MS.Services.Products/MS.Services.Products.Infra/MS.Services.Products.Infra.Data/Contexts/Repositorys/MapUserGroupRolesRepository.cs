﻿using Microsoft.EntityFrameworkCore;
using MS.Services.Products.Core.Domain.DbContexts.Entities;
using MS.Services.Products.Core.Domain.DbContexts.Repositorys;
using MS.Services.Products.Infra.Data.Contexts.Repositorys.Base;

namespace MS.Services.Products.Infra.Data.Contexts.Repositorys;

public class MapUserGroupRolesRepository : Repository<MapUserGroupRoles>, ISearchMapUserGroupRolesRepository
{
    public MapUserGroupRolesRepository(AuthDbContext applicationDbContext) : base(applicationDbContext)
    {

    }

    public async Task<List<Role>> GetRolesByUserGroup(string userGroupId)
    {
        return await this.AsQueriable().Include(c => c.Role)
                .Where(p => p.UserGroupId.ToString() == userGroupId)
                .Select(a => a.Role)
                .ToListAsync();
    }
}