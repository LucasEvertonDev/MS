using MS.Services.Auth.Core.Domain.Models.Auth;

namespace MS.Services.Auth.Core.Domain.Services.AuthServices
{
    public interface ILoginService
    {
        TokenModel TokenRetorno { get; set; }

        Task ExecuteAsync(LoginDto param);
    }
}