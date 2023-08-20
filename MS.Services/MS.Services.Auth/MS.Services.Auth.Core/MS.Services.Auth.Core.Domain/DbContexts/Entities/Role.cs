using MS.Services.Auth.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities;

public class Role : BaseEntityBasic
{
    public string Name { get; set; }

    public ICollection<MapUserGroupRoles> MapUserGroupRoles { get; set; }
}
