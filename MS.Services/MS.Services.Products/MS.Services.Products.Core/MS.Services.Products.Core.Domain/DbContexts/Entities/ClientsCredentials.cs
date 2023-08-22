using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Products.Core.Domain.DbContexts.Entities;

public class ClientsCredentials : BaseEntity
{
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string ClientDescription { get; set; }
}
