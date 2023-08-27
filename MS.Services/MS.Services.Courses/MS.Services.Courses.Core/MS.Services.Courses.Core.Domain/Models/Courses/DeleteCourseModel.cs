using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Courses.Core.Domain.Models.Courses;

public class DeleteCourseDto : BaseModel
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public string Id { get; set; }
}

public class DeletedCourseModel : BaseModel
{
    [DefaultValue("true")]
    public bool Sucess { get; set; }
}
