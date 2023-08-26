using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.Core.Domain.Services.UserServices;

public interface IUpdatePasswordService
{
    Task ExecuteAsync(UpdatePasswordUserDto param);
}