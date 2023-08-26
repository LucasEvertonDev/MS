using MS.Libs.Core.Application.Services;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Infra.Claims;
using MS.Libs.Infra.Utils.Exceptions;
using MS.Libs.Infra.Utils.Extensions;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.DbContexts.Repositorys;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Plugins.JWT;
using MS.Services.Auth.Core.Domain.Services.AuthServices;

namespace MS.Services.Auth.Core.Application.Services.AuthServices;

public class RefreshTokenService : BaseService<RefreshTokenDto>, IRefreshTokenService
{
    private readonly ISearchRepository<User> _searchUserRepository;
    private readonly ISearchMapUserGroupRolesRepository _mapuserGroupSearchRepository;
    private readonly ITokenService _tokenService;
    private readonly IUpdateRepository<User> _updateUserRepository;
    private readonly ISearchRepository<ClientCredentials> _searchClientCredentials;

    public TokenModel TokenRetorno { get; private set; }

    public RefreshTokenService(IServiceProvider serviceProvider,
        ISearchRepository<User> searchUserRepository,
        IUpdateRepository<User> updateUserRepository,
        ISearchRepository<ClientCredentials> searchClientCredentials,
        ISearchMapUserGroupRolesRepository mapuserGroupSearchRepository,
        ITokenService tokenService
    )
        : base(serviceProvider)
    {
        _searchUserRepository = searchUserRepository;
        _mapuserGroupSearchRepository = mapuserGroupSearchRepository;
        _tokenService = tokenService;
        _updateUserRepository = updateUserRepository;
        _searchClientCredentials = searchClientCredentials;
    }

    public override async Task ExecuteAsync(RefreshTokenDto refreshTokenDto)
    {
        await OnTransactionAsync(async () =>
        {
            await ValidateAsync(refreshTokenDto);

            var user = await _searchUserRepository.FirstOrDefaultAsync(user => user.Id.ToString() == _identity.GetUserClaim(JWTUserClaims.UserId));

            user.LastAuthentication = DateTime.Now;

            var roles = await _mapuserGroupSearchRepository.GetRolesByUserGroup(user.UserGroupId.ToString());

            var (tokem, data) = await _tokenService.GenerateToken(user, refreshTokenDto.ClientId, roles);

            await _updateUserRepository.UpdateAsync(user);

            this.TokenRetorno = new TokenModel
            {
                TokenJWT = tokem,
                DataExpiracao = data.ToLocalTime()
            };
        });
    }

    protected override async Task ValidateAsync(RefreshTokenDto refreshTokenDto)
    {
        if ((await _searchClientCredentials.GetListFromCacheAsync(a => a.ClientId == refreshTokenDto.ClientId && a.ClientSecret == refreshTokenDto.ClientSecret)) == null)
        {
            BusinessException(AuthErrors.Business.CLIENT_CREDENTIALS_INVALID);
        }

        var user = await _searchUserRepository.FirstOrDefaultAsync(user => user.Id.ToString() == _identity.GetUserClaim(JWTUserClaims.UserId));

        if (user == null)
        {
            throw new BusinessException(AuthErrors.Business.INVALID_REFRESH_TOKEN);
        }
    }
}
