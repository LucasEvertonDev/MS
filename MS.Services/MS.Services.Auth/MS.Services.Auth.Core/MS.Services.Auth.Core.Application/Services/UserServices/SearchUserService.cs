using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.UserServices;

namespace MS.Services.Auth.Core.Application.Services.UserServices;

public class SearchUserService : BaseService<SeacrhUserDto>, ISearchUserService
{
    private readonly ISearchRepository<User> _searchUserRepository;

    public PagedResult<SearchedUserModel> SearchedUsers { get; private set; }

    public SearchUserService(IServiceProvider serviceProvider,
        ISearchRepository<User> searchUserRepository) : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
    }

    public override async Task ExecuteAsync(SeacrhUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            var pagedResult = await _searchUserRepository.ToListAsync(param.PageNumber, param.PageSize, u => string.IsNullOrEmpty(param.Name) || u.Name.Contains(param.Name));

            this.SearchedUsers = _imapper.Map<PagedResult<SearchedUserModel>>(pagedResult);
        });
    }
}
