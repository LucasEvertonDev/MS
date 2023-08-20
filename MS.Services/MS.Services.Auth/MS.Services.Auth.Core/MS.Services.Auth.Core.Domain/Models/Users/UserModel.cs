using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Auth.Core.Domain.Models.Users;

public class UserModel : BaseModel, IRegisterUser, IUserRegistered
{
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid UserGroupId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime? LastAuthentication { get; set; }
}
