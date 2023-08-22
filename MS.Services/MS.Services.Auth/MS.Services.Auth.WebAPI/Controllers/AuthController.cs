using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Core.Domain.Services.AuthServices;
using MS.Services.Auth.Core.Domain.Services.UserServices;

namespace MS.Services.Auth.WebAPI.Controllers;

[Route("api/v1/products")]
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


    [HttpPut("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(UpdatedUserModel), StatusCodes.Status200OK)]
    public ActionResult Put(UpdateUserDto loginModel)
    {
        return Ok(new UpdatedUserModel());
    }
}
