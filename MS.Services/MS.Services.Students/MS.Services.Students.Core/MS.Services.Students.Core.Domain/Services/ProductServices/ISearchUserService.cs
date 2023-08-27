using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Students.Core.Domain.Models.Students;

namespace MS.Services.Students.Core.Domain.Services.StudentServices
{
    public interface ISearchStudentService
    {
        PagedResult<SearchedStudentModel> SearchedStudents { get; }

        Task ExecuteAsync(SeacrhStudentDto param);
    }
}