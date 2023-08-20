using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities.Base;

public class BaseEntityBasic : IEntity
{
    public Guid Id { get; set; }

    public int Situation { get; set; }
}
