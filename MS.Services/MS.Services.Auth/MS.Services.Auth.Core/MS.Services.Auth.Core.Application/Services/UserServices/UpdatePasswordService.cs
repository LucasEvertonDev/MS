using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Services.UserServices;

namespace MS.Services.Auth.Core.Application.Services.UserServices;

public class UpdatePasswordService : BaseService<UpdatePasswordUserDto>, IUpdatePasswordService
{
    private readonly ISearchRepository<User> _searchUserRepository;
    private readonly IPasswordHash _passwordHash;
    private readonly IUpdateRepository<User> _updateUserRepository;

    public UpdatePasswordService(IServiceProvider serviceProvider,
        ISearchRepository<User> searchUserRepository,
        IPasswordHash passwordHash,
        IUpdateRepository<User> updateUserRepository
    ) : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
        _passwordHash = passwordHash;
        _updateUserRepository = updateUserRepository;
    }

    public override async Task ExecuteAsync(UpdatePasswordUserDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var user = await _searchUserRepository.FirstOrDefaultAsync(user => user.Id.ToString() == param.Id);

            user.PasswordHash = _passwordHash.GeneratePasswordHash();
            user.Password = _passwordHash.EncryptPassword(param.Body.Password, user.PasswordHash);

            user = await _updateUserRepository.UpdateAsync(user);
        });
    }

    protected override async Task ValidateAsync(UpdatePasswordUserDto param)
    {
        if ((await _searchUserRepository.FirstOrDefaultAsync(u => u.Id.ToString() == param.Id)) == null)
        {
            throw new BusinessException(UserErrors.Business.USER_NOT_FOUND);
        }
    }
}
