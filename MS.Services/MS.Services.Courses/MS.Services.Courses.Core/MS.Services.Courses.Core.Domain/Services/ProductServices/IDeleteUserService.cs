using MS.Services.Courses.Core.Domain.Models.Courses;

namespace MS.Services.Courses.Core.Domain.Services.Courseservices
{
    public interface IDeleteCourseservice
    {
        Task ExecuteAsync(DeleteCourseDto param);
    }
}