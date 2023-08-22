using Microsoft.Extensions.DependencyInjection;
using MS.Services.Auth.Core.Domain.Services.AuthServices;
using MS.Services.Auth.Test.Infrastructure;

namespace MS.Services.Auth.Test.Services.AuthServices;

public class LoginServiceTest : BaseTest
{
    private readonly ILoginService _loginService;

    public LoginServiceTest()
    { 
        _loginService = _serviceProvider.GetService<ILoginService>();
    }

    [Fact]
    public async Task Test1()
    {
        await _loginService.ExecuteAsync(new Core.Domain.Models.Auth.LoginModel { Username = "lcseverton", Password = "123456" });

        var retorno = _loginService.TokenRetorno;
    }
}