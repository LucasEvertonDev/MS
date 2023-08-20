using MS.Libs.Core.Domain.Models.Base;

namespace MS.Services.Auth.Core.Domain.Models.Auth;

public class AuthModel : BaseModel, IAuth
{
    public string Username { get; set; }
    public string Password { get; set; }
}
