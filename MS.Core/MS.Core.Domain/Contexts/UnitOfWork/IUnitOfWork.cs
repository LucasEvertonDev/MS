namespace MS.Core.Domain.Contexts.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}
