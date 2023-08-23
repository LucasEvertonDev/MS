using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Core.Domain.Plugins.JWT
{
    public interface ITokenService
    {
        Task<(string, DateTime)> GenerateToken(User user, List<Role> roles);
    }
}