using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Libs.Infra.Data.Context.Configurations;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;

namespace MS.Services.Auth.Infra.Data.Contexts.Configurations
{
    public class ContractConfiguration : BaseEntityBasicConfiguration<Contract>
    {
        public override void Configure(EntityTypeBuilder<Contract> builder)
        {
            base.Configure(builder);

            builder
                .ToTable("Contracts")
                .HasDiscriminator(a => a.ClassName)
                .HasValue<MobileContract>(string.Concat(typeof(MobileContract).Namespace, ".", typeof(MobileContract).Name))
                .HasValue<TvContract>(string.Concat(typeof(TvContract).Namespace, ".", typeof(TvContract).Name))
                .HasValue<BroadBandContract>(string.Concat(typeof(BroadBandContract).Namespace, ".", typeof(BroadBandContract).Name));
        }
    }
}
