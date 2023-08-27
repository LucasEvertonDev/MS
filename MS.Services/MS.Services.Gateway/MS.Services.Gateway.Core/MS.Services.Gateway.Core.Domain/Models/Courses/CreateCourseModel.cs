using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Gateway.Core.Domain.Models.Courses;

public class CreateCourseDto : BaseModel
{
    [FromHeader(Name = "Authorization")]
    public string Token { get; set; }
    [FromBody] 
    public CreateCourseModel Body { get; set; }
}

public class CreateCourseModel : BaseModel
{
    [DefaultValue("Adminstração")]
    public string Name { get; set; }
    [DefaultValue("2023-01-01")]
    public DateTime StartDate { get; set; }
    [DefaultValue("2023-12-31")]
    public DateTime EndDate { get; set; }
}

public class CreatedCourseModel : BaseModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }
    [DefaultValue("Adminstração")]
    public string Name { get; set; }
    [DefaultValue("2023-01-01")]
    public DateTime StartDate { get; set; }
    [DefaultValue("2023-12-31")]
    public DateTime EndDate { get; set; }
}
