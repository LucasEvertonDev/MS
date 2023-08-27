using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Gateway.Core.Domain.Models.Students;

public class DeleteStudentDto : BaseModel
{
    [FromHeader(Name = "Authorization")]
    public string Token { get; set; }

    [JsonIgnore]
    [FromRoute(Name = "id")]
    public string Id { get; set; }
}

public class DeletedStudentModel : BaseModel
{
    [DefaultValue("true")]
    public bool Sucess { get; set; }
}
