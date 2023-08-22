using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Products.Core.Domain.Models.Auth;
using MS.Services.Products.Core.Domain.Models.Users;
using MS.Services.Products.Core.Domain.Services.AuthServices;
using MS.Services.Products.Core.Domain.Services.UserServices;

namespace MS.Services.Products.WebAPI.Controllers;

[Route("api/v1/auth")]
public class AuthController : BaseController
{
    private readonly ILoginService _loginService;
    private readonly ICreateUserService _createUserService;

    public AuthController(ICreateUserService createUserService,
         ILoginService loginservice)
    {
        _loginService = loginservice;
        _createUserService = createUserService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(CreatedUserModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] CreateUserModel createUserModel)
    {
        await _createUserService.ExecuteAsync(createUserModel);

        return Ok(_createUserService.CreatedUser);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginModel), StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
    {
        await _loginService.ExecuteAsync(loginModel);

        return Ok(_loginService.TokenRetorno);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdatedUserModel), StatusCodes.Status200OK)]
    public ActionResult Put(UpdateUserDto loginModel)
    {
        return Ok(new UpdatedUserModel());
    }
}
