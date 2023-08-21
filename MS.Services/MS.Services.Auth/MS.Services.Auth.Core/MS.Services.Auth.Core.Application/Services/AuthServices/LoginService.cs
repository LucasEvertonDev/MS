using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Plugins.Cryptography;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using MS.Services.Auth.Core.Domain.Services.AuthServices;

namespace MS.Services.Auth.Core.Application.Services.AuthServices;

public class LoginService : BaseService<LoginModel>, ILoginService
{
    private readonly ISearchRepository<User> _userSearchRepository;
    private readonly IPasswordHash _passwordHash;
    private readonly ITokenService _tokenService;
    private readonly ISearchMapUserGroupRolesRepository _mapuserGroupSearchRepository;

    public TokenModel TokenRetorno { get; set; }

    public LoginService(IServiceProvider serviceProvider,
        ISearchRepository<User> userSearchRepository,
        IPasswordHash passwordHash,
        ISearchRepository<Role> rolesSearchRepository,
        ITokenService tokenService,
        ISearchMapUserGroupRolesRepository mapuserGroupSearchRepository) : base(serviceProvider)
    {
        _userSearchRepository = userSearchRepository;
        _passwordHash = passwordHash;
        _tokenService = tokenService;
        _mapuserGroupSearchRepository = mapuserGroupSearchRepository;
    }

    public async override Task ExecuteAsync(LoginModel param)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(param);

            var user = await _userSearchRepository.FirstOrDefaultAsync(a => a.Username == param.Username);

            var roles = await _mapuserGroupSearchRepository.GetRolesByUserGroup(user.UserGroupId.ToString());

            var tokem = await _tokenService.GenerateToken(user, roles);

            TokenRetorno = new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = DateTime.Now.AddHours(2)
            };
        });
    }

    protected override async Task ValidateAsync(LoginModel param)
    {
        var user = await _userSearchRepository.FirstOrDefaultAsync(a => a.Username == param.Username);
        if (user == null || string.IsNullOrEmpty(user.Id.ToString()))
        {
            throw new BusinessException("Login ou senha inválidos!");
        }

        if (!_passwordHash.PasswordIsEquals(param.Password, user?.PasswordHash, user?.Password))
        {
            throw new BusinessException("Login ou senha inválidos!");
        }
    }
}
