namespace MS.Libs.Core.Domain.DbContexts.UnitOfWork;

public interface IUnitOfWork
{
    Task CommitAsync();
    Task RollbackAsync();
}
