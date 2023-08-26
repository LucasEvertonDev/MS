using MS.Services.Auth.Core.Domain.Models.Auth;

namespace MS.Services.Auth.Core.Domain.Services.AuthServices
{
    public interface IRefreshTokenService
    {
        TokenModel TokenRetorno { get; }

        Task ExecuteAsync(RefreshTokenDto refreshTokenDto);
    }
}