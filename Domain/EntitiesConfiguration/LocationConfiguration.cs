using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public sealed class LocationConfiguration : IEntityTypeConfiguration<LocationRecord>
{
    public void Configure(EntityTypeBuilder<LocationRecord> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(e => e.Tariffication).WithMany(x => x.Locations);
    }
}