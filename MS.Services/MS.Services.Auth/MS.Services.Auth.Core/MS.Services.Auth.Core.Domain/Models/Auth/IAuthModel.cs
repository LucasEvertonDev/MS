using System.ComponentModel;

namespace MS.Services.Auth.Core.Domain.Models.Auth;

public interface IAuth
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }
    [DefaultValue("123456")]
    public string Password { get; set; }
}
