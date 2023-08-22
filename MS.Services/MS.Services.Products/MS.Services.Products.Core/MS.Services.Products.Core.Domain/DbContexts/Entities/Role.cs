using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Products.Core.Domain.DbContexts.Entities;

public class Role : BaseEntityBasic
{
    public string Name { get; set; }

    public ICollection<MapUserGroupRoles> MapUserGroupRoles { get; set; }
}
