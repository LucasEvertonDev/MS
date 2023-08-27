using MS.Services.Courses.Core.Domain.Models.Courses;

namespace MS.Services.Courses.Core.Domain.Services.Courseservices
{
    public interface ICreateCourseservice
    {
        CreatedCourseModel CreatedCourse { get; set; }

        Task ExecuteAsync(CreateCourseModel param);
    }
}