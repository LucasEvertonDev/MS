
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Infra.Utils.Extensions;
using MS.Services.Auth.Core.Domain.Models;

namespace MS.Services.Auth.Core.Application.Services;

public abstract class BaseService<TDto> where TDto : IServiceDTO
{
    private readonly IUnitOfWork _unitOfWork;
    protected readonly IMapperPlugin _imapper;

    public BaseService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
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

    public abstract Task<TDto> ExecuteAsync(TDto serviceDTO);

    public abstract Task ValidateAsync(TDto serviceDTO);
}
