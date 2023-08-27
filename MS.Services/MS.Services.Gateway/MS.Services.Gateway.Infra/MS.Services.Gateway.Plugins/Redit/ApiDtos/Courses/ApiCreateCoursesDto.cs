﻿using System.ComponentModel;

namespace MS.Services.Gateway.Plugins.Redit.ApiDtos.Courses;

public class ApiCreateCoursesDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
