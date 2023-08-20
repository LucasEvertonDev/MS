using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Core.Domain.Plugins.JWT
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user, List<Role> roles);
    }
}