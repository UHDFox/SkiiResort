using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.Tariff;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class TariffConfiguration : IEntityTypeConfiguration<TariffRecord>
{
    public void Configure(EntityTypeBuilder<TariffRecord> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(s => s.Skipasses).WithOne(t => t.Tariff);

        builder.HasMany(tr => tr.Tariffications).WithOne(t => t.Tariff);
    }
}
