using MS.Libs.Core.Domain.Services;

namespace MS.Libs.Core.Application.Services.Common;

public abstract class ActionService<Tparam, Tresult> : BaseService, IActionService<Tparam, Tresult>
{
    public ActionService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
    public abstract Task<Tresult> ExecuteAsync(Tparam param);

    protected virtual Task ValidateAsync(Tparam param)
    {
        return Task.CompletedTask;
    }
}
