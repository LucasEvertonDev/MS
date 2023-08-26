using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Core.Domain.DbContexts.Enuns;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts.Configurations;

public class ClientCredentialsConfiguration : BaseEntityConfiguration<ClientCredentials>
{
    public override void Configure(EntityTypeBuilder<ClientCredentials> builder)
    {
        base.Configure(builder);

        builder.ToTable("ClientsCredentials");

        builder.Property(u => u.ClientId).HasMaxLength(50).IsRequired();

        builder.Property(u => u.ClientSecret).HasMaxLength(300).IsRequired();

        builder.Property(u => u.ClientDescription).HasMaxLength(300);

        DefaultData(builder);
    }

    public void DefaultData(EntityTypeBuilder<ClientCredentials> builder)
    {
        builder.HasData(new ClientCredentials
        {
            Id = new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
            ClientId = "7064bbbf-5d11-4782-9009-95e5a6fd6822",
            ClientSecret = "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910",
            ClientDescription = "Cliente padrão da aplicação",
            CreateDate = new DateTime(2023, 08, 26),
            Situation = (int)Situation.Active
        });
    }
}
