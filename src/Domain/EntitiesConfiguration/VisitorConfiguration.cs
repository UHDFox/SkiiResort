using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Domain.EntitiesConfiguration;

public sealed class VisitorConfiguration : IEntityTypeConfiguration<VisitorRecord>
{
    public void Configure(EntityTypeBuilder<VisitorRecord> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(s => s.Skipasses).WithOne(v => v.Visitor);

        builder.HasMany(v => v.Skipasses)
            .WithOne(s => s.Visitor)
            .HasForeignKey(s => s.VisitorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
