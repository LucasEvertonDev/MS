using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Core.Domain.Services.UserServices
{
    public interface ICreateUserService
    {
        CreatedUserModel CreatedUser { get; set; }

        Task ExecuteAsync(CreateUserModel param);
    }
}