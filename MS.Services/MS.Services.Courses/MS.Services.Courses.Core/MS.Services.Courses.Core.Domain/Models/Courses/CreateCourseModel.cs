﻿using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Courses.Core.Domain.Models.Courses;

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
