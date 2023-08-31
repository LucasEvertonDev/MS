using Microsoft.AspNetCore.Mvc;
using Refit;

namespace MS.Services.Gateway.Plugins.Redit.ApiDtos.Courses;

public class ApiSearchCoursesDto
{
    public ApiSearchCoursesDto() { }

    //[AliasAs("pagenumber")]
    //public int PageNumber { get; set; }

    //[AliasAs("pagesize")]
    //public int PageSize { get; set; }

    [Query("name")]
    public string Name { get; set; }
}
