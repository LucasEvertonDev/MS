using MS.Libs.Core.Domain.DbContexts.Entities.Base;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Libs.Core.Application.Services;
public class BaseService
{
    private readonly IUnitOfWork _unitOfWork;

    public BaseService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
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
            TRetorno retorno = await func();
            await _unitOfWork.CommitAsync();
            return retorno;
        }
        catch
        {
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    protected void ValidateSearchEntityId(BaseEntityBasic entityBasic)
    {
        if (entityBasic == null || string.IsNullOrEmpty(entityBasic.Id.ToString()))
        {
            throw new BusinessException("Não existe nenhuma entidade para o ide informado.");
        }
    }
}
