using MS.Services.Products.Core.Domain.Models.Auth;

namespace MS.Services.Products.Core.Domain.Services.AuthServices
{
    public interface ILoginService
    {
        TokenModel TokenRetorno { get; set; }

        Task ExecuteAsync(LoginModel param);
    }
}