using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Students.Core.Domain.Contansts;
using MS.Services.Students.Core.Domain.DbContexts.Entities;
using MS.Services.Students.Core.Domain.Models.Students;
using MS.Services.Students.Core.Domain.Services.StudentServices;

namespace MS.Services.Students.Core.Application.Services.StudentServices;

public class UpdateStudentService : BaseService<UpdateStudentDto>, IUpdateStudentService
{
    private readonly IUpdateRepository<Student> _updateStudentRepository;
    private readonly ISearchRepository<Student> _searchStudentRepository;

    public UpdatedStudentModel UpdatedStudent { get; set; }

    public UpdateStudentService(IServiceProvider serviceProvider,
        IUpdateRepository<Student> updateStudentRepository,
        ISearchRepository<Student> searchStudentRepository
    ) : base(serviceProvider)
    {
        _updateStudentRepository = updateStudentRepository;
        _searchStudentRepository = searchStudentRepository;
    }

    public override async Task ExecuteAsync(UpdateStudentDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var Student = await _searchStudentRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            Student = _imapper.Map(param.Body, Student);

            Student = await _updateStudentRepository.UpdateAsync(Student);

            this.UpdatedStudent = _imapper.Map<UpdatedStudentModel>(Student);
        });
    }

    protected override async Task ValidateAsync(UpdateStudentDto param)
    {
        if ((await _searchStudentRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            throw new BusinessException(StudentErrors.Business.STUDENT_NOT_FOUND);
        }

        if (_searchStudentRepository.AsQueriable().Where(u => u.Cpf == param.Body.Cpf).Any())
        {
            BusinessException(StudentErrors.Business.ALREADY_STUDENT);
        }
    }
}
