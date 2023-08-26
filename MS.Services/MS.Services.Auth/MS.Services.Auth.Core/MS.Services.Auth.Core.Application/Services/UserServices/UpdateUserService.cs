using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.UserServices;

namespace MS.Services.Auth.Core.Application.Services.UserServices;

public class UpdateUserService : BaseService<UpdateUserDto>, IUpdateUserService
{
    private readonly IUpdateRepository<User> _updateUserRepository;
    private readonly ISearchRepository<User> _searchUserRepository;
    private readonly IValidatorModel<UpdateUserModel> _validatorUpdateuserModel;
    private readonly ISearchRepository<UserGroup> _searchUserGroupRepository;

    public UpdatedUserModel UpdatedUser { get; set; }

    public UpdateUserService(IServiceProvider serviceProvider,
        IUpdateRepository<User> updateUserRepository,
        ISearchRepository<User> searchUserRepository,
        ISearchRepository<UserGroup> searchUserGroupRepository,
        IValidatorModel<UpdateUserModel> validatorUpdateuserModel
    ) : base(serviceProvider)
    {
        _updateUserRepository = updateUserRepository;
        _searchUserRepository = searchUserRepository;
        _validatorUpdateuserModel = validatorUpdateuserModel;
        _searchUserGroupRepository = searchUserGroupRepository;
    }

    public override async Task ExecuteAsync(UpdateUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var user = await _searchUserRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id);

            user = _imapper.Map(param.Body, user);

            user = await _updateUserRepository.UpdateAsync(user);

            this.UpdatedUser = _imapper.Map<UpdatedUserModel>(user);
        });
    }

    protected override async Task ValidateAsync(UpdateUserDto param)
    {
        if ((await _searchUserRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            throw new BusinessException(UserErrors.Business.USER_NOT_FOUND);
        }

        if ((await _searchUserRepository.ToListAsync(u => u.Id.ToString() != param.Id && u.Username == param.Body.Username)).Any())
        {
            throw new BusinessException(UserErrors.Business.ALREADY_USERNAME);
        }

        if ((await _searchUserRepository.ToListAsync(u => u.Id.ToString() != param.Id && u.Username == param.Body.Username)).Any())
        {
            throw new BusinessException(UserErrors.Business.ALREADY_EMAIL);
        }

        await _validatorUpdateuserModel.ValidateModelAsync(param.Body);
    }
}
