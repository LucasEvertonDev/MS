using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Core.Domain.Services.UserServices
{
    public interface IUpdateUserService
    {
        UpdatedUserModel UpdatedUser { get; set; }

        Task ExecuteAsync(UpdateUserDto param);
    }
}