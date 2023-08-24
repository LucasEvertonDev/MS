using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.AuthServices;
using MS.Services.Auth.Core.Domain.Services.UserServices;
using System.Security.Claims;

namespace MS.Services.Auth.WebAPI.Controllers;

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

    /// <summary>
    /// Registra o usuário
    /// </summary>
    /// <param name="createUserModel"></param>
    /// <returns></returns>
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
    [Authorize]
    [ProducesResponseType(typeof(UpdatedUserModel), StatusCodes.Status200OK)]
    public ActionResult Put(UpdateUserDto loginModel)
    {
        var claimsIdentity = User.Identity as ClaimsIdentity;

        // alternatively
        // claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

        // get some claim by type
        var someClaim = claimsIdentity.FindFirst("some-claim");

        // iterate all claims
        foreach (var claim in claimsIdentity.Claims)
        {
            System.Console.WriteLine(claim.Type + ":" + claim.Value);
        }

        return Ok(new UpdatedUserModel());
    }
}

