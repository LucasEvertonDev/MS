using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Libs.WebApi.HttpContainers;
using MS.Services.Auth.Core.Domain.Models;

namespace MS.Services.Auth.WebAPI.Controllers
{
    [Route("api/v2/[Controller]")]
    public class AuthController : BaseController
    {
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

        /// <summary>
        /// Rota para logar com um usuário
        /// </summary>
        /// <param name="requestDTO"></param>
        /// <returns></returns>
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
