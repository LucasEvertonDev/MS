using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Courses.Core.Domain.Contansts;
using MS.Services.Courses.Core.Domain.DbContexts.Entities;
using MS.Services.Courses.Core.Domain.Models.Courses;
using MS.Services.Courses.Core.Domain.Services.Courseservices;

namespace MS.Services.Courses.Core.Application.Services.Courseservices;

public class UpdateCourseservice : BaseService<UpdateCourseDto>, IUpdateCourseservice
{
    private readonly IUpdateRepository<Course> _updateCourseRepository;
    private readonly ISearchRepository<Course> _searchCourseRepository;

    public UpdatedCourseModel UpdatedCourse { get; set; }

    public UpdateCourseservice(IServiceProvider serviceProvider,
        IUpdateRepository<Course> updateCourseRepository,
        ISearchRepository<Course> searchCourseRepository
    ) : base(serviceProvider)
    {
        _updateCourseRepository = updateCourseRepository;
        _searchCourseRepository = searchCourseRepository;
    }

    public override async Task ExecuteAsync(UpdateCourseDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var Course = await _searchCourseRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            Course = _imapper.Map(param.Body, Course);

            Course = await _updateCourseRepository.UpdateAsync(Course);

            this.UpdatedCourse = _imapper.Map<UpdatedCourseModel>(Course);
        });
    }

    protected override async Task ValidateAsync(UpdateCourseDto param)
    {
        if ((await _searchCourseRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            throw new BusinessException(CourseErrors.Business.COURSE_NOT_FOUND);
        }

        if (_searchCourseRepository.AsQueriable().Where(u => u.Name == param.Body.Name).Any())
        {
            BusinessException(CourseErrors.Business.ALREADY_COURSE);
        }
    }
}
