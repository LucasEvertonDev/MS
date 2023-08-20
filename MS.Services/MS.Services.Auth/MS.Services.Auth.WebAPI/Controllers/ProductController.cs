using Microsoft.AspNetCore.Mvc;

namespace MS.Services.Auth.WebAPI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
