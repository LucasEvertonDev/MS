using MS.Services.Students.Core.Domain.Models.Students;

namespace MS.Services.Students.Core.Domain.Services.StudentServices
{
    public interface ICreateStudentService
    {
        CreatedStudentModel CreatedStudent { get; set; }

        Task ExecuteAsync(CreateStudentModel param);
    }
}