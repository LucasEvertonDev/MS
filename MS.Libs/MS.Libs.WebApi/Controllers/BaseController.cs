using Microsoft.AspNetCore.Mvc;

namespace MS.Libs.WebApi.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
[Produces("application/json")]
public class BaseController : ControllerBase
{

}
