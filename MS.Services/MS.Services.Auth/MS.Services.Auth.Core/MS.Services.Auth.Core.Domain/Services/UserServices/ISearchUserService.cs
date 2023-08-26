using MS.Libs.Core.Domain.Models.Base;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Core.Domain.Services.UserServices
{
    public interface ISearchUserService
    {
        PagedResult<SearchedUserModel> SearchedUsers { get; }

        Task ExecuteAsync(SeacrhUserDto param);
    }
}