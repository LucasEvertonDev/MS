using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.WebAPI.Controllers
{
    public class UseTpcMappingStrategyController : Controller
    {
        public IActionResult Index()
        {
            ///var criar só a tabela animal e dog os camps duplica tudo mas quando consulta ele vira animal se quiser 

            ////modelBuilder.Entity<Animal>().UseTpcMappingStrategy();
            ////modelBuilder.Entity<Cat>().ToTable("Cats");
            ////modelBuilder.Entity<Dog>().ToTable("Dogs");
            return View();
        }
    }
}
