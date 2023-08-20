namespace MS.Libs.Core.Domain.Services;

public interface IActionService<Tparam, Tresult>
{
    Task<Tresult> ExecuteAsync(Tparam param);
}
