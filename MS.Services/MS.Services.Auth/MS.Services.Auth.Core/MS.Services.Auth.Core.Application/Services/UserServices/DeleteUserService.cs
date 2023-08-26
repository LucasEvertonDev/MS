using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.UserServices;

namespace MS.Services.Auth.Core.Application.Services.UserServices;

public class DeleteUserService : BaseService<DeleteUserDto>, IDeleteUserService
{
    private readonly ISearchRepository<User> _userSearchRepository;
    private readonly IDeleteRepository<User> _deleteUserRepository;

    public DeleteUserService(IServiceProvider serviceProvider,
        ISearchRepository<User> userSearchRepository,
        IDeleteRepository<User> deleteUserRepository
    ) : base(serviceProvider)
    {
        _userSearchRepository = userSearchRepository;
        _deleteUserRepository = deleteUserRepository;
    }

    public override async Task ExecuteAsync(DeleteUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            await _deleteUserRepository.DeleteLogicAsync(user => user.Id.ToString() == param.Id);
        });
    }

    protected override async Task ValidateAsync(DeleteUserDto param)
    {
        if ((await _userSearchRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            BusinessException(UserErrors.Business.USER_NOT_FOUND);
        }
    }
}
