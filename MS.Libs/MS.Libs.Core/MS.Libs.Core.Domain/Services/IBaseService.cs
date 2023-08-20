namespace MS.Libs.Core.Domain.Services;

public interface IBaseService<TParam, TResult>
{
    public Task<TResult> ExecuteAsync(TParam param);

    protected Task ValidateAsync(TParam param);
}

