using Microsoft.Extensions.Logging;
using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using MS.Services.Auth.Core.Domain.Services.AuthServices;

namespace MS.Services.Auth.Core.Application.Services.AuthServices;

public class LoginService : BaseService<LoginDto>, ILoginService
{
    private readonly ISearchRepository<User> _userSearchRepository;
    private readonly IPasswordHash _passwordHash;
    private readonly ITokenService _tokenService;
    private readonly ISearchMapUserGroupRolesRepository _mapuserGroupSearchRepository;
    private readonly IUpdateRepository<User> _updateUserRepository;
    private readonly ISearchRepository<ClientCredentials> _searchClientCredentials;

    public TokenModel TokenRetorno { get; set; }

    public LoginService(IServiceProvider serviceProvider,
        ISearchRepository<User> userSearchRepository,
        IUpdateRepository<User> updateUserRepository,
        IPasswordHash passwordHash,
        ITokenService tokenService,
        ISearchRepository<ClientCredentials> searchClientCredentials,
        ISearchMapUserGroupRolesRepository mapuserGroupSearchRepository,
        ILogger<LoginService> login) : base(serviceProvider)
    {
        _userSearchRepository = userSearchRepository;
        _passwordHash = passwordHash;
        _tokenService = tokenService;
        _mapuserGroupSearchRepository = mapuserGroupSearchRepository;
        _updateUserRepository = updateUserRepository;
        _searchClientCredentials = searchClientCredentials;
        login.LogWarning("TEste de warning");
    }

    public async override Task ExecuteAsync(LoginDto param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var user = await _userSearchRepository.FirstOrDefaultAsync(a => a.Username == param.Body.Username);

            user.LastAuthentication = DateTime.Now;

            var roles = await _mapuserGroupSearchRepository.GetRolesByUserGroup(user.UserGroupId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, param.ClientId, roles);

            await _updateUserRepository.UpdateAsync(user);

            TokenRetorno = new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = data.ToLocalTime()
            };
        });
    }

    protected override async Task ValidateAsync(LoginDto param)
    {
        if (!(await _searchClientCredentials.GetListFromCacheAsync(a => a.ClientId == param.ClientId && a.ClientSecret == param.ClientSecret)).Any())
        {
            BusinessException(AuthErrors.Business.CLIENT_CREDENTIALS_INVALID);
        }

        var user = await _userSearchRepository.FirstOrDefaultAsync(a => a.Username == param.Body.Username);
        if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
        {
            BusinessException(AuthErrors.Business.INVALID_LOGIN);
        }

        if (!_passwordHash.PasswordIsEquals(param.Body.Password, user?.PasswordHash, user?.Password))
        {
            BusinessException(AuthErrors.Business.INVALID_LOGIN);
        }
    }
}
