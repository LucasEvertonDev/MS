using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.HttpContainers;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Models.Token;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.WebAPI.Controllers;

public class AuthController : BaseController
{
    private readonly ICreateService<UserModel> _createUserService;

    public AuthController(ICreateService<UserModel> createUserService)
    {
        _createUserService = createUserService;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseDTO<IRegisterUser>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] RequestDTO<IRegisterUser> requestDTO)
    {
        var retorno = await _createUserService.ExecuteAsync((UserModel)requestDTO.Body);

        var response = new ResponseDTO<IUserRegistered>()
        {
            Content = retorno,
            Sucess = true
        };

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDTO<IToken>), StatusCodes.Status200OK)]
    public ActionResult Login([FromBody] RequestDTO<IAuth> requestDTO)
    {
        var response = new ResponseDTO<IToken>()
        {
            Sucess = true
        };
        return Ok(response);
    }
}
