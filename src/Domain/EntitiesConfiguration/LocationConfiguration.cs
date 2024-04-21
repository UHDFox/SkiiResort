using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.Location;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class LocationConfiguration : IEntityTypeConfiguration<LocationRecord>
{
    public void Configure(EntityTypeBuilder<LocationRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(e => e.Tariffications).WithOne(e => e.Location);
    }
}
