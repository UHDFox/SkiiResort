using Domain.Entities.Skipass;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public class SkipassConfiguration : IEntityTypeConfiguration<SkipassRecord>
{
    public void Configure(EntityTypeBuilder<SkipassRecord> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(t => t.Tariff);
        builder.HasOne(v => v.Visitor);
    }
}