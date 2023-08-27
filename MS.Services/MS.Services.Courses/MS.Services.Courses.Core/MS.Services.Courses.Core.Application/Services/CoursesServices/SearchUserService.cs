using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Courses.Core.Domain.DbContexts.Entities;
using MS.Services.Courses.Core.Domain.Models.Courses;
using MS.Services.Courses.Core.Domain.Services.Courseservices;

namespace MS.Services.Courses.Core.Application.Services.Courseservices;

public class SearchCourseservice : BaseService<SeacrhCourseDto>, ISearchCourseservice
{
    private readonly ISearchRepository<Course> _searchCourseRepository;

    public PagedResult<SearchedCourseModel> SearchedCourses { get; private set; }

    public SearchCourseservice(IServiceProvider serviceProvider,
        ISearchRepository<Course> searchCourseRepository) : base(serviceProvider)
    {
        _searchCourseRepository = searchCourseRepository;
    }

    public override async Task ExecuteAsync(SeacrhCourseDto param)
    {
        await OnTransactionAsync(async () =>
        {
            var pagedResult = await _searchCourseRepository.ToListAsync(param.PageNumber, param.PageSize, u => string.IsNullOrEmpty(param.Name) || u.Name.Contains(param.Name));

            this.SearchedCourses = _imapper.Map<PagedResult<SearchedCourseModel>>(pagedResult);
        });
    }
}
