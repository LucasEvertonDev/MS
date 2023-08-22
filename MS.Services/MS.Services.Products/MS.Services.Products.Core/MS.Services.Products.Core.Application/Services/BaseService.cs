
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Infra.Utils.Extensions;

namespace MS.Services.Products.Core.Application.Services;

public abstract class BaseService<TModel> where TModel : IModel
{
    private readonly IUnitOfWork _unitOfWork;
    protected readonly IMapperPlugin _imapper;

    public BaseService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
    }

    public abstract Task ExecuteAsync(TModel param);

    protected virtual Task ValidateAsync(TModel param)
    {
        return Task.CompletedTask;
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
}
