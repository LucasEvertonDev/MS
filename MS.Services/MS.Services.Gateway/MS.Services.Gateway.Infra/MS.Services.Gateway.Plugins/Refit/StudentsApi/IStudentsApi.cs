using MS.Services.Gateway.Plugins.Redit.ApiDtos.Courses;
using Refit;

namespace MS.Services.Gateway.Plugins.Redit;

public interface IStudentsApi
{
    [Headers("Content-Type: application/json")]
    [Get("/api/v1/students/{pagenumber}/{pagesize}")]
    Task<dynamic> Get([Header("Authorization")] string token, int pagenumber, int pagesize, ApiSearchStudentsDto apiSearchCoursesDto);

    [Headers("Content-Type: application/json")]
    [Post("/api/v1/students")]
    Task<dynamic> Post([Header("Authorization")] string token, [Body] ApiCreateStudentsDto apiSearchCoursesDto);

    [Headers("Content-Type: application/json")]
    [Put("/api/v1/students/{id}")]
    Task<dynamic> Put([Header("Authorization")] string token, string id, [Body] ApiUpdateStudentsDto apiUpdateCoursesDto);

    [Headers("Content-Type: application/json")]
    [Delete("/api/v1/students/{id}")]
    Task<dynamic> Delete([Header("Authorization")] string token, string id);
}
