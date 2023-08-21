using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Auth.Core.Domain.Models.Auth;

public class LoginModel : IModel
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }
    [DefaultValue("123456")]
    public string Password { get; set; }
}


public class TokenModel : IModel
{
    public string TokenJWT { get; set; }
    public DateTime DataExpiracao { get; set; }
}