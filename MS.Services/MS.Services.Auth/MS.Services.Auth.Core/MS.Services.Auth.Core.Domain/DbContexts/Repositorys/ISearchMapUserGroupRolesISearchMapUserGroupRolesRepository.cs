using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Core.Domain.DbContexts.Repositorys;

public interface ISearchMapUserGroupRolesRepository : ISearchRepository<MapUserGroupRoles>
{
    Task<List<Role>> GetRolesByUserGroup(string userGroupId);
}
