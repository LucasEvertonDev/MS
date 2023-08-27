using MS.Services.Students.Core.Domain.Models.Students;

namespace MS.Services.Students.Core.Domain.Services.StudentServices
{
    public interface IDeleteStudentService
    {
        Task ExecuteAsync(DeleteStudentDto param);
    }
}