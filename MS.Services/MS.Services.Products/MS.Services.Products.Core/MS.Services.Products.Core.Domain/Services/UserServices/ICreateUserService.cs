using MS.Services.Products.Core.Domain.Models.Users;

namespace MS.Services.Products.Core.Domain.Services.UserServices
{
    public interface ICreateUserService
    {
        CreatedUserModel CreatedUser { get; set; }

        Task ExecuteAsync(CreateUserModel param);
    }
}