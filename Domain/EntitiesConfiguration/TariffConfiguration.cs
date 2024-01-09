using Domain.Entities.Tariff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public class TariffConfiguration : IEntityTypeConfiguration<TariffRecord>
{
    public void Configure(EntityTypeBuilder<TariffRecord> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasKey(e => e.Name);
    }
}