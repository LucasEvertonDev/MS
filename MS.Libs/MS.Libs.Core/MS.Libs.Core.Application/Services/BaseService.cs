using Microsoft.AspNetCore.Http;
using MS.Libs.Core.Domain.DbContexts.UnitOfWork;
using MS.Libs.Core.Domain.Models.Base;
using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Core.Domain.Plugins.IMappers;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;
using System;
using System.Security.Principal;

namespace MS.Libs.Core.Application.Services;

public abstract class BaseService<TParam>
{
    private readonly IUnitOfWork _unitOfWork;
    protected readonly IMapperPlugin _imapper;
    protected readonly IIdentity? _identity;
    public BaseService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
        _identity = serviceProvider.GetService<IHttpContextAccessor>().HttpContext.User?.Identity;
    }

    public abstract Task ExecuteAsync(TParam param);

    protected virtual Task ValidateAsync(TParam param)
    {
        return Task.CompletedTask;
    }
    protected virtual Task OnSucess()
    {
        return Task.CompletedTask;
    }

    protected async virtual Task OnError(Exception exception)
    {
        await _unitOfWork.RollbackAsync();
        throw exception;
    }

    protected virtual void BusinessException(ErrorModel errorModel)
    {
        throw new BusinessException(errorModel);
    }

    public async Task OnTransactionAsync(Func<Task> func)
    {
        try
        {
            await func();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception exception)
        {
            await OnError(exception);
        }
        finally
        {
            await OnSucess();
        }
    }

    public async Task<TRetorno?> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func)
    {
        try
        {
            TRetorno retorno = await func();
            await _unitOfWork.CommitAsync();
            return retorno;
        }
        catch (Exception exception)
        {
            await OnError(exception);
            return default(TRetorno);
        }
        finally
        {
            await OnSucess();
        }
    }
}

public abstract class BaseService
{
    private readonly IUnitOfWork _unitOfWork;
    protected readonly IMapperPlugin _imapper;
    protected readonly IIdentity? _identity;
    public BaseService(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
        _imapper = serviceProvider.GetService<IMapperPlugin>();
        _identity = serviceProvider.GetService<IHttpContextAccessor>().HttpContext.User?.Identity;
    }

    public abstract Task ExecuteAsync();

    protected virtual Task ValidateAsync()
    {
        return Task.CompletedTask;
    }
    protected virtual Task OnSucess()
    {
        return Task.CompletedTask;
    }

    protected async virtual Task OnError(Exception exception)
    {
        await _unitOfWork.RollbackAsync();
        throw exception;
    }

    public async Task OnTransactionAsync(Func<Task> func)
    {
        try
        {
            await func();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception exception)
        {
            await OnError(exception);
        }
        finally
        {
            await OnSucess();
        }
    }

    public async Task<TRetorno?> OnTransactionAsync<TRetorno>(Func<Task<TRetorno>> func)
    {
        try
        {
            TRetorno retorno = await func();
            await _unitOfWork.CommitAsync();
            return retorno;
        }
        catch (Exception exception)
        {
            await OnError(exception);
            return default(TRetorno);
        }
        finally
        {
            await OnSucess();
        }
    }
}

