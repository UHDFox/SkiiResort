using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public sealed class VisitorConfiguration : IEntityTypeConfiguration<VisitorRecord>
{
    public void Configure(EntityTypeBuilder<VisitorRecord> builder)
    {
        builder.HasKey(e => e.Id);
       
        builder.HasMany(s => s.Skipasses).WithOne(v => v.Visitor);
    }
}