using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Auth.Core.Domain.Models.ServiceDtos.Users
{
    public class CreateUserDTO : IServiceDTO<CreateUserModel, CreatedUserModel>
    {
        public CreateUserModel Request { get; set ; }
        public CreatedUserModel Response { get; set; }
    }

    public class CreateUserModel : IModel
    {
        [DefaultValue("lcseverton")]
        public string Username { get; set; }
        [DefaultValue("123456")]
        public string Password { get; set; }
        [DefaultValue("F97E565D-08AF-4281-BC11-C0206EAE06FA")]
        public Guid UserGroupId { get; set; }
        [DefaultValue("Lucas Everton Santos de Oliveira")]
        public string Name { get; set; }
        [DefaultValue("lcseverton@gmail.com")]
        public string Email { get; set; }
    }

    public class CreatedUserModel : CreateUserModel
    {
        [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
        public string Id { get; set; }
    }
}
