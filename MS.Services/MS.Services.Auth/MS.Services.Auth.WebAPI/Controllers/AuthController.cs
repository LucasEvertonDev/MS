using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.HttpContainers;
using MS.Services.Auth.Core.Domain.Models;
using MS.Services.Auth.Infra.Data.Context;

namespace MS.Services.Auth.WebAPI.Controllers
{
    [Route("api/v2/[Controller]")]
    public class AuthController : BaseController
    {
        public AuthController(AuthDbContext authDbContext)
        {

        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(ResponseDTO<TokenModel>), StatusCodes.Status200OK)]
        public ActionResult Register([FromBody] RequestDTO<LoginModel> requestDTO)
        {
            var response = new ResponseDTO<TokenModel>()
            {
                Sucess = true
            };

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ResponseDTO<TokenModel>), StatusCodes.Status200OK)]
        public ActionResult Login([FromBody] RequestDTO<LoginModel> requestDTO)
        {
            var response = new ResponseDTO<TokenModel>()
            {
                Sucess = true
            };

            return Ok(response);
        }
    }
}
