using MS.Services.Auth.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }
    public Guid UserGroupId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime LastAuthentication { get; set; }
    public UserGroup UserGroup { get; set; }
}
