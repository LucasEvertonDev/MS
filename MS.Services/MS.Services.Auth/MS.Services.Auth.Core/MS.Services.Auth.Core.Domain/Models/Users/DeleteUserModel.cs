using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Auth.Core.Domain.Models.Users;

public class DeleteUserDto : BaseModel
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public string Id { get; set; }
}

public class DeletedUserModel : BaseModel
{
    [DefaultValue("true")]
    public bool Sucess { get; set; }
}
