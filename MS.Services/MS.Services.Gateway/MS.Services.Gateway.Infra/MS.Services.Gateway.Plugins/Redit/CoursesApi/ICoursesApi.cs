using MS.Services.Gateway.Plugins.Redit.ApiDtos.Courses;
using Refit;

namespace MS.Services.Gateway.Plugins.Redit.CoursesApi;

public interface ICoursesApi
{
    [Headers("Content-Type: application/json")]
    [Get("/api/v1/courses/{pagenumber}/{pagesize}")]
    Task<dynamic> Get([Header("Authorization")] string token, int pagenumber, int pagesize, ApiSearchCoursesDto apiSearchCoursesDto);
}
