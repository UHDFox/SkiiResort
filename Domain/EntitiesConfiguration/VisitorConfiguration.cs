using Domain.Entities.Visitor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.EntitiesConfiguration;

public class VisitorConfiguration : IEntityTypeConfiguration<VisitorRecord>
{
    public void Configure(EntityTypeBuilder<VisitorRecord> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasKey(e => e.Passport);
        builder.HasKey(e => e.Name);
    }
}