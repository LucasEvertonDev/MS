using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Services;

namespace MS.Libs.Core.Application.Services;

public class BaseService
{
    private readonly IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnTransactionAsync(Func<Task> func)
    {
        try
        {
            await func();

            await _unitOfWork.CommitAsync();
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<TRetorno> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func)
    {
        try
        {
            var retorno = await func();

            await _unitOfWork.CommitAsync();

            return retorno;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }
}