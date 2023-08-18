namespace MS.Libs.Core.Domain.DbContexts.Entities.Base;

public class BaseEntity : IEntity
{
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
