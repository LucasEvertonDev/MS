using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Services;
using MS.Libs.Core.Domain.Services.Crud;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.HttpContainers;
using MS.Services.Auth.Core.Domain.Models.Auth;
using MS.Services.Auth.Core.Domain.Models.Users;

namespace MS.Services.Auth.WebAPI.Controllers;

[Route("api/v1/auth")]
public class AuthController : BaseController
{
    private readonly IActionService<AuthModel, TokenModel> _loginService;
    private readonly ICreateService<UserModel> _createUserService;

    public AuthController(ICreateService<UserModel> createUserService,
         IActionService<AuthModel, TokenModel> loginservice)
    {
        _loginService = loginservice;
        _createUserService = createUserService;
    }


    /// <summary>

    /// </summary>
    /// <param name="requestDTO"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseDTO<UserModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] RequestDTO<UserModel> requestDTO)
    {
        var retorno = await _createUserService.ExecuteAsync(requestDTO.Body);

        var response = new ResponseDTO<UserModel>()
        {
            Content = retorno,
            Sucess = true
        };

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ResponseDTO<AuthModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] RequestDTO<AuthModel> requestDTO)
    {
        var retorno = await _loginService.ExecuteAsync(requestDTO.Body);

        var response = new ResponseDTO<TokenModel>()
        {
            Content = retorno,
            Sucess = true
        };
        return Ok(response);
    }
}
