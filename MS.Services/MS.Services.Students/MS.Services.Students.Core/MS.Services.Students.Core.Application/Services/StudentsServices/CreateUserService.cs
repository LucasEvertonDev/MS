using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Services.Students.Core.Domain.Contansts;
using MS.Services.Students.Core.Domain.DbContexts.Entities;
using MS.Services.Students.Core.Domain.Models.Students;
using MS.Services.Students.Core.Domain.Services.StudentServices;

namespace MS.Services.Students.Core.Application.Services.StudentServices
{
    public class CreateStudentService : BaseService<CreateStudentModel>, ICreateStudentService
    {
        private readonly ISearchRepository<Student> _searchRepository;
        private readonly ICreateRepository<Student> _createRepository;

        public CreatedStudentModel CreatedStudent { get; set; }

        public CreateStudentService(IServiceProvider serviceProvider,
            ISearchRepository<Student> searchRepository,
            ICreateRepository<Student> createRepository) : base(serviceProvider)
        {
            _createRepository = createRepository;
            _searchRepository = searchRepository;
        }

        public override async Task ExecuteAsync(CreateStudentModel param)
        {
            await OnTransactionAsync(async () =>
            {
                await ValidateAsync(param);

                var Student = _imapper.Map<Student>(param);

                Student = await _createRepository.CreateAsync(Student);

                this.CreatedStudent = _imapper.Map<CreatedStudentModel>(Student);
            });
        }

        protected override Task ValidateAsync(CreateStudentModel param)
        {
            if (_searchRepository.AsQueriable().Where(u => u.Cpf == param.Cpf).Any())
            {
                BusinessException(StudentErrors.Business.ALREADY_STUDENT);
            }

            return Task.CompletedTask;
        }
    }
}
