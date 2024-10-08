using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class SkipassConfiguration : IEntityTypeConfiguration<SkipassRecord>
{
    public void Configure(EntityTypeBuilder<SkipassRecord> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(v => v.Visitor).WithMany(s => s.Skipasses);

        builder.HasMany(e => e.VisitorActions).WithOne(s => s.Skipass);
    }
}
