using Domain.Entities.Tariffication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public class TarifficationConfiguration : IEntityTypeConfiguration<TarifficationRecord>
{
    public void Configure(EntityTypeBuilder<TarifficationRecord> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Tariff).WithMany(e => e.Tariffications);
        
        //builder.HasMany(e => e.)
    }
}