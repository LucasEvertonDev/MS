using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Auth.Core.Domain.Models.Users;

public class UpdateUserDto : BaseModel
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual string Id { get; set; }

    [FromBody]
    public UpdateUserModel Body { get; set; }
}

public class UpdateUserModel : BaseModel
{
    [DefaultValue("lcseverton")]
    public string Username { get; set; }

    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public string UserGroupId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Name { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }
}

public class UpdatedUserModel : BaseModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }
    [DefaultValue("lcseverton")]
    public string Username { get; set; }

    [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
    public Guid UserGroupId { get; set; }
    [DefaultValue("Lucas Everton Santos de Oliveira")]
    public string Name { get; set; }
    [DefaultValue("lcseverton@gmail.com")]
    public string Email { get; set; }
}
