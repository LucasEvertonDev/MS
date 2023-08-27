using MS.Services.Students.Core.Domain.Models.Students;

namespace MS.Services.Students.Core.Domain.Services.StudentServices
{
    public interface IUpdateStudentService
    {
        UpdatedStudentModel UpdatedStudent { get; set; }

        Task ExecuteAsync(UpdateStudentDto param);
    }
}