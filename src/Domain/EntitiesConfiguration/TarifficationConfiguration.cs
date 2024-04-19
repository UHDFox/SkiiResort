using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Domain.EntitiesConfiguration;

public class TarifficationConfiguration : IEntityTypeConfiguration<TarifficationRecord>
{
    public void Configure(EntityTypeBuilder<TarifficationRecord> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
