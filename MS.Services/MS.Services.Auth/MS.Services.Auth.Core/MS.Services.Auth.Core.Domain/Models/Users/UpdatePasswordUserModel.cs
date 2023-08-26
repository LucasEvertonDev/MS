using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Auth.Core.Domain.Models.Users;

public class UpdatePasswordUserDto : BaseModel
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual string Id { get; set; }

    [FromBody]
    public UpdatePasswordUserModel Body { get; set; }
}

public class UpdatePasswordUserModel : BaseModel
{
    [DefaultValue("123456")]
    public string Password { get; set; }
}

public class UpdatedPasswordUserModel : BaseModel
{
    public bool Sucess { get; set; }
}
