using MS.Libs.Core.Application.Services.Common;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Services;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;

namespace MS.Services.Auth.Core.Application.Services.UserServices;

public class CreateUserService : CreateService<UserModel, User>, ICreateService<UserModel>
{
    private readonly ISearchRepository<User> _searchRepository;
    private readonly IPasswordHash _passwordHash;

    public CreateUserService(IServiceProvider serviceProvider,
        ISearchRepository<User> searchRepository,
        IPasswordHash passwordHash) : base(serviceProvider)
    {
        _searchRepository = searchRepository;
        _passwordHash = passwordHash;
    }

    public override async Task<UserModel> ExecuteAsync(UserModel param)
    {
        return await OnTransactionAsync(async () =>
        {
            var user = _imapper.Map<User>(param);

            await ValidateAsync(param);

            user.PasswordHash = _passwordHash.GeneratePasswordHash();
            user.Password = _passwordHash.EncryptPassword(user.Password, user.PasswordHash);

            user = await _createRepository.CreateAsync(user);

            return _imapper.Map<UserModel>(user);
        });
    }

    protected override async Task ValidateAsync(UserModel param)
    {
        if (_searchRepository.Queryable().Where(u => u.Username == param.Username).Any())
        {
            throw new BusinessException("There is already a registered user with the entered username.");
        }

        if (_searchRepository.Queryable().Where(u => u.Email == param.Email).Any())
        {
            throw new BusinessException("There is already a registered user with the email provided");
        }

        await base.ValidateAsync(param);
    }
}
