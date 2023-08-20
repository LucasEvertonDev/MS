using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.Services;
using MS.Services.Auth.Core.Domain.Models.Auth;
using System.Threading.Tasks;

namespace MS.Services.Auth.Core.Application.Services.AuthServices;

public class LoginService : BaseService, IBaseService<AuthModel, TokenModel>
{
    public LoginService(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public Task<TokenModel> ExecuteAsync(AuthModel param)
    {
        throw new NotImplementedException();
    }

    protected Task ValidateAsync(AuthModel param)
    {
        throw new NotImplementedException();
    }
}
