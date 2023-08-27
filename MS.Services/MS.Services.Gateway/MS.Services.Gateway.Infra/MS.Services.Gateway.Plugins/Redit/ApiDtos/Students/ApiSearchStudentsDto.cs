using Microsoft.AspNetCore.Mvc;
using Refit;

namespace MS.Services.Gateway.Plugins.Redit.ApiDtos.Courses;

public class ApiSearchStudentsDto
{
    public ApiSearchStudentsDto() { }

    [Query("name")]
    public string Name { get; set; }
}
