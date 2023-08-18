using MS.Libs.Core.Domain.DbContexts.UnitOfWork;

namespace MS.Libs.Infra.Data.Context.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BaseDbContext _context;

    public UnitOfWork(BaseDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        await _context.DisposeAsync();
    }
}
