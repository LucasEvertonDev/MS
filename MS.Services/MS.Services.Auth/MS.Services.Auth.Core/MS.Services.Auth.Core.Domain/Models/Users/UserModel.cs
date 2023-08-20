using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Auth.Core.Domain.Attributes;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Auth.Core.Domain.Models.Users;

[Reflect<User>]
public class UserModel : BaseModel
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }
    [DefaultValue("123456")]
    public string Password { get; set; }
    [DefaultValue("PasswordHash")]
    public string PasswordHash { get; set; }
    [DefaultValue("123456")]
    public Guid UserGroupId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Name { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }
    [DefaultValue("null")]
    public DateTime? LastAuthentication { get; set; }
}
