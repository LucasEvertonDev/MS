using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Services.Courses.Core.Domain.Contansts;
using MS.Services.Courses.Core.Domain.DbContexts.Entities;
using MS.Services.Courses.Core.Domain.Models.Courses;
using MS.Services.Courses.Core.Domain.Services.Courseservices;

namespace MS.Services.Courses.Core.Application.Services.Courseservices;

public class DeleteCourseservice : BaseService<DeleteCourseDto>, IDeleteCourseservice
{
    private readonly ISearchRepository<Course> _CoursesearchRepository;
    private readonly IDeleteRepository<Course> _deleteCourseRepository;

    public DeleteCourseservice(IServiceProvider serviceProvider,
        ISearchRepository<Course> CoursesearchRepository,
        IDeleteRepository<Course> deleteCourseRepository
    ) : base(serviceProvider)
    {
        _CoursesearchRepository = CoursesearchRepository;
        _deleteCourseRepository = deleteCourseRepository;
    }

    public override async Task ExecuteAsync(DeleteCourseDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            await _deleteCourseRepository.DeleteLogicAsync(Course => Course.Id.ToString() == param.Id);
        });
    }

    protected override async Task ValidateAsync(DeleteCourseDto param)
    {
        if ((await _CoursesearchRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            BusinessException(CourseErrors.Business.COURSE_NOT_FOUND);
        }
    }
}
