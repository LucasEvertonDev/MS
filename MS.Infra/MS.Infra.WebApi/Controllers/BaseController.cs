using Microsoft.AspNetCore.Mvc;

namespace MS.Infra.WebApi.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
[Produces("application/json")]
public class BaseController : ControllerBase
{

}
