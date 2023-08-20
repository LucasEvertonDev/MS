using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities.Base;

public class BaseEntity : BaseEntityBasic
{
    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
