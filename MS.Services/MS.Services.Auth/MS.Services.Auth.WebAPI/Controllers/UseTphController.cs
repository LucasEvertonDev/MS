using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Infra.Data.Contexts;

namespace MS.Services.Auth.WebAPI.Controllers
{
    public class UseTphController : BaseController
    {
        public AuthDbContext context { get; set; }
        public UseTphController(AuthDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Criar")]
        public List<Contract> Criar()
        {
            var mammal = new MobileContract
            {
                Charge = 1212,
                MobileNumber = "1111111",
                Months = 1,
                StartDate = DateTime.Now,
            };
            context.Contracts.Add(mammal);

            var bird = new TvContract
            {
                Charge = 1212,
                PackageType = PackageType.S,
                Months = 1,
                StartDate = DateTime.Now,
            };

            context.TvContracts.Add(bird);

            context.SaveChanges();


            return context.Contracts.ToList();
        }

        [HttpGet("MobileContract")]
        public List<MobileContract> MobileContract()
        {
            return context.Contracts.OfType<MobileContract>().ToList();
        }

        [HttpGet("TvContract")]
        public List<TvContract> TvContract()
        {
            return context.Contracts.OfType<TvContract>().ToList();
        }
    }
}
