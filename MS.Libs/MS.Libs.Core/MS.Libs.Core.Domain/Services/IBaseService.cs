namespace MS.Libs.Core.Domain.Services;

public interface IBaseService<TParam, TResult>
{
    public Task<TResult> ExecuteAsync(TParam param);

    protected Task ValidateAsync(TParam param);
}

public interface IBaseService<TResult>
{
    public Task<TResult> ExecuteAsync();

    protected Task ValidateAsync();
}
