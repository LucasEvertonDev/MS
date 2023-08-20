using MS.Services.Auth.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Auth.Core.Domain.DbContexts.Entities;

public class ClientsCredentials : BaseEntity
{
    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string ClientDescription { get; set; }
}
