using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Courses.Core.Domain.Models.Courses;

namespace MS.Services.Courses.Core.Domain.Services.Courseservices
{
    public interface ISearchCourseservice
    {
        PagedResult<SearchedCourseModel> SearchedCourses { get; }

        Task ExecuteAsync(SeacrhCourseDto param);
    }
}