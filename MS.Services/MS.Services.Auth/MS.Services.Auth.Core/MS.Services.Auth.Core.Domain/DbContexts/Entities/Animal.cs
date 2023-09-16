using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities
{
    public class Animal : BaseEntityBasic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class Mammal : Animal
    {
        public string Type { get; set; }
    }
    public class Bird : Animal
    {
        public int Wingspan { get; set; }
    }
}
