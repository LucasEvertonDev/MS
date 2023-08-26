using MS.Libs.Core.Domain.Models.Error;
using MS.Services.Auth.Core.Domain.Contansts.Auth;
using MS.Services.Auth.Core.Domain.Contansts.User;

namespace MS.Services.Auth.Core.Domain.Contansts;

public class AuthErrors
{
    public static AuthBusinessErrors Business = new AuthBusinessErrors(); 
}

public class UserErrors
{
    public static AuthValidatorsErrors Validators = new AuthValidatorsErrors();
    
    public static UserBusinessErrors Business = new UserBusinessErrors();
}
