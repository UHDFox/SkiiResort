using Domain.Entities.Skipass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public class SkipassConfiguration : IEntityTypeConfiguration<SkipassRecord>
{
    public void Configure(EntityTypeBuilder<SkipassRecord> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(t => t.TariffRecord);
        builder.HasOne(v => v.VisitorRecord);
    }
}