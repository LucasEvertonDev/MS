using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities;

public class MapUserGroupRoles : BaseEntityBasic
{
    public Guid UserGroupId { get; set; }
    public Guid RoleId { get; set; }
    public UserGroup UserGroup { get; set; }
    public Role Role { get; set; }
}
