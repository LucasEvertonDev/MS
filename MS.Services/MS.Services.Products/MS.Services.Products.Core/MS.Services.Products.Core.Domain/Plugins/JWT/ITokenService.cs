using MS.Services.Products.Core.Domain.DbContexts.Entities;

namespace MS.Services.Products.Core.Domain.Plugins.JWT
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user, List<Role> roles);
    }
}