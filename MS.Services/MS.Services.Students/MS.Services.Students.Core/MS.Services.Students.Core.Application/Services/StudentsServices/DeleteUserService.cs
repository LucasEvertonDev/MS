using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Services.Students.Core.Domain.Contansts;
using MS.Services.Students.Core.Domain.DbContexts.Entities;
using MS.Services.Students.Core.Domain.Models.Students;
using MS.Services.Students.Core.Domain.Services.StudentServices;

namespace MS.Services.Students.Core.Application.Services.StudentServices;

public class DeleteStudentService : BaseService<DeleteStudentDto>, IDeleteStudentService
{
    private readonly ISearchRepository<Student> _StudentSearchRepository;
    private readonly IDeleteRepository<Student> _deleteStudentRepository;

    public DeleteStudentService(IServiceProvider serviceProvider,
        ISearchRepository<Student> StudentSearchRepository,
        IDeleteRepository<Student> deleteStudentRepository
    ) : base(serviceProvider)
    {
        _StudentSearchRepository = StudentSearchRepository;
        _deleteStudentRepository = deleteStudentRepository;
    }

    public override async Task ExecuteAsync(DeleteStudentDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            await _deleteStudentRepository.DeleteLogicAsync(Student => Student.Id.ToString() == param.Id);
        });
    }

    protected override async Task ValidateAsync(DeleteStudentDto param)
    {
        if ((await _StudentSearchRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            BusinessException(StudentErrors.Business.STUDENT_NOT_FOUND);
        }
    }
}
