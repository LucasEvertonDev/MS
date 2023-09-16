using Microsoft.AspNetCore.Mvc;
using MS.Libs.WebApi.Controllers;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Infra.Data.Contexts;

namespace MS.Services.Auth.WebAPI.Controllers
{
    [Route("api/v1/teste")]

    public class UseTptMappingStrategyController : BaseController
    {
        public AuthDbContext context { get; set; }  
        public UseTptMappingStrategyController(AuthDbContext context) 
        {
            this.context = context;
        }

        [HttpGet("Criar")]
        public List<Animal> Criar()
        {
            var mammal = new Mammal
            {
                Name = "Lion",
                Type = "Carnivore"
            };
            context.Mammals.Add(mammal);

            var bird = new Bird
            {
                Name = "Eagle",
                Wingspan = 6
            };

            context.Birds.Add(bird);

            context.SaveChanges();


            return context.Animals.ToList();
        }

        [HttpGet("GetBird")]
        public List<Bird> GetBird()
        {
            return context.Animals.OfType<Bird>().ToList();
        }

        [HttpGet("GetMammal")]
        public List<Mammal> GetMammal()
        {
            return context.Animals.OfType<Mammal>().ToList();
        }
    }
}
