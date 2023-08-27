using MS.Services.Gateway.Plugins.Redit.ApiDtos.Courses;
using Refit;

namespace MS.Services.Gateway.Plugins.Redit.CoursesApi;

public interface ICoursesApi
{
    [Headers("Content-Type: application/json")]
    [Get("/api/v1/courses/{pagenumber}/{pagesize}")]
    Task<dynamic> Get([Header("Authorization")] string token, int pagenumber, int pagesize, ApiSearchCoursesDto apiSearchCoursesDto);

    [Headers("Content-Type: application/json")]
    [Post("/api/v1/courses")]
    Task<dynamic> Post([Header("Authorization")] string token, [Body] ApiCreateCoursesDto apiSearchCoursesDto);

    [Headers("Content-Type: application/json")]
    [Put("/api/v1/courses/{id}")]
    Task<dynamic> Put([Header("Authorization")] string token, string id, [Body] ApiUpdateCoursesDto apiUpdateCoursesDto);

    [Headers("Content-Type: application/json")]
    [Delete("/api/v1/courses/{id}")]
    Task<dynamic> Delete([Header("Authorization")] string token, string id);
}
