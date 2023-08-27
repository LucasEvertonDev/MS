using MS.Services.Courses.Core.Domain.Models.Courses;

namespace MS.Services.Courses.Core.Domain.Services.Courseservices
{
    public interface IUpdateCourseservice
    {
        UpdatedCourseModel UpdatedCourse { get; set; }

        Task ExecuteAsync(UpdateCourseDto param);
    }
}