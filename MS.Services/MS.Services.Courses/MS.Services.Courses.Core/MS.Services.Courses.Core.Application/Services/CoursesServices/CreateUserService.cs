using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Services.Courses.Core.Domain.Contansts;
using MS.Services.Courses.Core.Domain.DbContexts.Entities;
using MS.Services.Courses.Core.Domain.Models.Courses;
using MS.Services.Courses.Core.Domain.Services.Courseservices;

namespace MS.Services.Courses.Core.Application.Services.Courseservices
{
    public class CreateCourseservice : BaseService<CreateCourseModel>, ICreateCourseservice
    {
        private readonly ISearchRepository<Course> _searchRepository;
        private readonly ICreateRepository<Course> _createRepository;

        public CreatedCourseModel CreatedCourse { get; set; }

        public CreateCourseservice(IServiceProvider serviceProvider,
            ISearchRepository<Course> searchRepository,
            ICreateRepository<Course> createRepository) : base(serviceProvider)
        {
            _createRepository = createRepository;
            _searchRepository = searchRepository;
        }

        public override async Task ExecuteAsync(CreateCourseModel param)
        {
            await OnTransactionAsync(async () =>
            {
                await ValidateAsync(param);

                var Course = _imapper.Map<Course>(param);

                Course = await _createRepository.CreateAsync(Course);

                this.CreatedCourse = _imapper.Map<CreatedCourseModel>(Course);
            });
        }

        protected override Task ValidateAsync(CreateCourseModel param)
        {
            if (_searchRepository.AsQueriable().Where(u => u.Name == param.Name).Any())
            {
                BusinessException(CourseErrors.Business.ALREADY_COURSE);
            }

            return Task.CompletedTask;
        }
    }
}
