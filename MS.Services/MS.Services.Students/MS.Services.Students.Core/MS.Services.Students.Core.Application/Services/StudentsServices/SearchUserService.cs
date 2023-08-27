using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Students.Core.Domain.DbContexts.Entities;
using MS.Services.Students.Core.Domain.Models.Students;
using MS.Services.Students.Core.Domain.Services.StudentServices;

namespace MS.Services.Students.Core.Application.Services.StudentServices;

public class SearchStudentService : BaseService<SeacrhStudentDto>, ISearchStudentService
{
    private readonly ISearchRepository<Student> _searchStudentRepository;

    public PagedResult<SearchedStudentModel> SearchedStudents { get; private set; }

    public SearchStudentService(IServiceProvider serviceProvider,
        ISearchRepository<Student> searchStudentRepository) : base(serviceProvider)
    {
        _searchStudentRepository = searchStudentRepository;
    }

    public override async Task ExecuteAsync(SeacrhStudentDto param)
    {
        await OnTransactionAsync(async () =>
        {
            var pagedResult = await _searchStudentRepository.ToListAsync(param.PageNumber, param.PageSize, u => string.IsNullOrEmpty(param.Name) || u.Name.Contains(param.Name));

            this.SearchedStudents = _imapper.Map<PagedResult<SearchedStudentModel>>(pagedResult);
        });
    }
}
